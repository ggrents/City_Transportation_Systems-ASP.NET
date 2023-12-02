using City_Transportation_Systems.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace City_Transportation_Systems.Controllers
{
    [Route("api/buses")]
    [ApiController]
    public class BusesController : Controller
    {
        private IBusRepository _busRepository;
        public BusesController(IBusRepository busRepository)
        {
            _busRepository = busRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetBuses()
        {
            var buses = await _busRepository.GetAllBusesAsync();
            return Ok(buses);
        }
    }
}
