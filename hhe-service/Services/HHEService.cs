using hhe_service.Data;
using hhe_service.Models;
using hhe_service.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace hhe_service.Services
{
    public class HHEService : IHHEService
    {
        private readonly TimeSpan ThreeSeconds = new TimeSpan(0,0,3);
        private IServiceScopeFactory _scopeFactory;
        public HHEService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task AddActivation(Activation activation)
        {
            var activationTime = activation.DispensedDateTime;
            await DbWrapper<bool>(async _db =>
            {
                var hhesForThatHour = await _db.HHEs
                    .Where(x => x.DeviceSerialNumber == activation.DeviceSerialNumber && (
                        ((x.StartTime.Date == activationTime.Date) && (x.StartTime.Hour == activationTime.Hour))
                        || ((x.EndTime.Date == activationTime.Date) && (x.EndTime.Hour == activationTime.Hour))
                     ))
                    .OrderBy(x => x.StartTime)
                    .ToListAsync();

                var existingHHE = hhesForThatHour
                                    .SingleOrDefault(x => 
                                        (x.StartTime - activationTime).Duration() <= ThreeSeconds || 
                                        (x.EndTime - activationTime).Duration() <= ThreeSeconds);

                if (existingHHE != null)
                {
                    if(activationTime < existingHHE.StartTime)
                        existingHHE.StartTime = activationTime;
                    else if(activationTime > existingHHE.EndTime)
                        existingHHE.EndTime = activationTime;
                    existingHHE.DispensedMicroLitre += activation.DispensedMicroLitre;
                }
                else
                {
                    var hhe = new HHE
                    {
                        Id = Guid.NewGuid(),
                        DeviceSerialNumber = activation.DeviceSerialNumber,
                        DispensedMicroLitre = activation.DispensedMicroLitre,
                        StartTime = activationTime,
                        EndTime = activationTime
                    };
                    await _db.AddAsync(hhe);
                }

                await _db.SaveChangesAsync();
                return true;
            });
        }

        public async Task<List<HHE>> GetHHEs()
        {
            return await DbWrapper<List<HHE>>(async _db => await _db.HHEs.ToListAsync());
        }

        private async Task<T> DbWrapper<T>(Func<HHEDbContext, Task<T>> func)
        {
            using(var scope = _scopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<HHEDbContext>();
                return await func(db);
            }
        }
    }
}
