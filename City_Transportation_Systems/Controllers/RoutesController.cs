using AutoMapper;
using City_Transportation_Systems.DTO;
using City_Transportation_Systems.Interfaces;
using City_Transportation_Systems.Models;
using City_Transportation_Systems.Repository;
using Microsoft.AspNetCore.Mvc;
using Route = City_Transportation_Systems.Models.Route;

namespace City_Transportation_Systems.Controllers
{
    [Route("api/routes")]
    [ApiController]
    public class RoutesController : Controller
    {
        private IRouteRepository _routeRepository;
        private ILogger _logger;
        private IMapper _mapper;
        public RoutesController(IRouteRepository routeRepository, ILogger<BusesController> logger, IMapper mapper)
        {
            _routeRepository = routeRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoutes()
        {
            var routes = await _routeRepository.GetAllRoutesAsync();
            return Ok(_mapper.Map<List<RouteDTO>>(routes));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRouteById(int id)
        {
            var route = await _routeRepository.GetRouteByIdAsync(id);
            if (route == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(_mapper.Map<RouteDTO>(route));
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddRoute([FromBody] CreateRouteDTO routeDto)
        {
            var route = _mapper.Map<Route>(routeDto);
            bool isCreated = await _routeRepository.CreateRouteAsync(route);



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
        public async Task<IActionResult> DeleteRoute(int id)
        {
            var route = await _routeRepository.GetRouteByIdAsync(id);
            if (route == null)
            {
                return NotFound();

            }
            var isDeleted = await _routeRepository.DeleteRouteAsync(route);
            if (isDeleted)
            {
                return Ok("Successfully deleted!");
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoute(RouteDTO routeDto)
        {
            var route = await _routeRepository.GetRouteByIdAsync(routeDto.Id);
            if (route == null)
            {
                return NotFound();

            }

            var isUpdated = await _routeRepository.UpdateRouteAsync(_mapper.Map<Route>(routeDto));
            if (isUpdated)
            {
                return Ok(routeDto);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
