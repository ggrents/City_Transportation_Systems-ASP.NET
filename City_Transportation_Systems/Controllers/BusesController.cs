using City_Transportation_Systems.Interfaces;
using City_Transportation_Systems.Models;
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

        [HttpPost]
        public async Task<IActionResult> AddBus([FromBody] Bus bus)
        {
            bool isCreated = await _busRepository.CreateBusAsync(bus);
            if (isCreated)
            {
                return Ok("Succesfully created!");
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBus(int id)
        {
            Bus bus = await _busRepository.GetBusByIdAsync(id);
            if (bus == null) {
                return NotFound();

            }
            var isDeleted = await _busRepository.DeleteBusAsync(bus);
            if (isDeleted)
            {
                return Ok("Successfully deleted!");
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetBusById(int id)
        {
            var bus = await _busRepository.GetBusByIdAsync(id);
            return Ok(bus);
        }
    }
}
