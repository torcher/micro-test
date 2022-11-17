using hhe_service.Models;
using hhe_service.Models.Entities;

namespace hhe_service.Services
{
    public interface IHHEService
    {
        public Task<List<HHE>> GetHHEs();

        public Task AddActivation(Activation activation);
    }
}
