using data_collector.Models;
using data_collector.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace data_collector.Controllers
{
    [Route("api/data-collector")]
    [ApiController]
    public class DataCollectorController : ControllerBase
    {
        private readonly ActivationProducer _activationProducer;
        public DataCollectorController(ActivationProducer activationProducer)
        {
            _activationProducer = activationProducer;
        }

        [HttpPost]
        public async Task<IActionResult> HandleActivationData(Activation activation)
        {
            try
            {
                await _activationProducer.Submit(Guid.NewGuid(), activation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok();
        }
    }
}
