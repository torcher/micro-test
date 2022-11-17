using hhe_service.Models.Entities;
using hhe_service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hhe_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HHEController : ControllerBase
    {
        private readonly IHHEService _hhes;
        public HHEController(IHHEService hhes)
        {
            _hhes = hhes;
        }

        [HttpGet]
        public async Task<List<HHE>> Index()
        {
            return await _hhes.GetHHEs();
        }
    }
}
