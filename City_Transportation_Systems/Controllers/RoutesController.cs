using Asp.Versioning;
using AutoMapper;
using City_Transportation_Systems.DTO;
using City_Transportation_Systems.Interfaces;
using City_Transportation_Systems.Models;
using City_Transportation_Systems.Repository;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
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
        public RoutesController(IRouteRepository routeRepository, ILogger<RoutesController> logger, IMapper mapper)
        {
            _routeRepository = routeRepository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить список маршрутов.
        /// </summary>
        [HttpGet]
        [SwaggerResponse(200, Type = typeof(List<RouteDTO>))]
        public async Task<IActionResult> GetRoutes()
        {
            var routes = await _routeRepository.GetAllRoutesAsync();
            return Ok(_mapper.Map<List<RouteDTO>>(routes));
        }

        /// <summary>
        /// Получить маршрут по Айди.
        /// </summary>
        [HttpGet("{id}")]
        [SwaggerResponse(200, Type = typeof(RouteDTO))]
        [SwaggerResponse(404, Type = typeof(string))]
        public async Task<IActionResult> GetRouteById(int id)
        {
            var route = await _routeRepository.GetRouteByIdAsync(id);
            if (route == null)
            {
                return NotFound("Route not found");
            }
            else
            {
                return Ok(_mapper.Map<RouteDTO>(route));
            }
        }


        /// <summary>
        /// Получить список маршрутов по Айди.
        /// </summary>
        [HttpGet("station/{StationId}")]
        [SwaggerResponse(200, Type = typeof(List<RouteDTO>))]
        [SwaggerResponse(404, Type = typeof(string))]
        public async Task<IActionResult> GetRouteByStation(int StationId)
        {
            var routes = await _routeRepository.GetRoutesByStation(StationId);
            if (routes == null)
            {
                return NotFound("Route not found");
            }
            else
            {
                return Ok(_mapper.Map<List<RouteDTO>>(routes));
            }
        }

        /// <summary>
        /// Добавить маршрут.
        /// </summary>
        [HttpPost]
        [SwaggerResponse(200, Type = typeof(string))]
        [SwaggerResponse(404, Type = typeof(string))]
        [SwaggerResponse(400)]
        public async Task<IActionResult> AddRoute([FromBody] CreateRouteDTO routeDto)
        {
            var route = _mapper.Map<Route>(routeDto);
            try
            {
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

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Удалить маршрут.
        /// </summary>
        [HttpDelete("{id}")]
        [SwaggerResponse(200, Type = typeof(string))]
        [SwaggerResponse(404, Type = typeof(string))]
        [SwaggerResponse(400)]
        public async Task<IActionResult> DeleteRoute(int id)
        {
            var route = await _routeRepository.GetRouteByIdAsync(id);
            if (route == null)
            {
                return NotFound("Route not found");

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

        /// <summary>
        /// Обновить маршрут.
        /// </summary>
        [HttpPut("{id}")]
        [SwaggerResponse(200, Type = typeof(RouteDTO))]
        [SwaggerResponse(404, Type = typeof(string))]
        [SwaggerResponse(400)]
        public async Task<IActionResult> UpdateRoute(RouteDTO routeDto)
        {
            var route = await _routeRepository.GetRouteByIdAsync(routeDto.Id);
            if (route == null)
            {
                return NotFound("Route not found");

            }
            try
            {
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

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
