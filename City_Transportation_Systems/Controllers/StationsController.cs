using AutoMapper;
using City_Transportation_Systems.DTO;
using City_Transportation_Systems.Interfaces;
using City_Transportation_Systems.Models;
using City_Transportation_Systems.Repository;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace City_Transportation_Systems.Controllers
{
    [Route("api/stations")]
    [ApiController]
    public class StationsController : Controller
    {
        private IStationRepository _stationRepository;
        private ILogger _logger;
        private IMapper _mapper;
        public StationsController(IStationRepository stationRepository, ILogger<StationsController> logger, IMapper mapper)
        {
            _stationRepository = stationRepository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить список остановок.
        /// </summary>
        [HttpGet]
        [SwaggerResponse(200, Type = typeof(List<StationDTO>))]
        [SwaggerResponse(404, Type = typeof(string))]
        public async Task<IActionResult> GetStations()
        {
            var stations = await _stationRepository.GetAllStationsAsync();
            return Ok(_mapper.Map<List<StationDTO>>(stations));
        }


        /// <summary>
        /// Получить остановку по айди.
        /// </summary>
        [HttpGet("{id}")]
        [SwaggerResponse(200, Type = typeof(StationDTO))]
        [SwaggerResponse(404, Type = typeof(string))]
        public async Task<IActionResult> GetStationById(int id)
        {
            var station = await _stationRepository.GetStationByIdAsync(id);
            if (station == null)
            {
                return NotFound("Station not found");
            }
            else
            {
                return Ok(_mapper.Map<StationDTO>(station));
            }
        }

        /// <summary>
        /// Получить список остановок по маршруту.
        /// </summary>
        [HttpGet("route/{RouteId}")]
        [SwaggerResponse(200, Type = typeof(List<StationDTO>))]
        [SwaggerResponse(404, Type = typeof(string))]
        public async Task<IActionResult> GetStationByRouteId(int RouteId)
        {
            var stations = await _stationRepository.GetStationsByRoute(RouteId);
            if (stations == null)
            {
                return NotFound("Stations not found");
            }
            else
            {
                return Ok(_mapper.Map<List<StationDTO>>(stations));
            }
        }

        /// <summary>
        /// Добавить остановку.
        /// </summary>
        [HttpPost]
        [SwaggerResponse(200, Type = typeof(string))]
        [SwaggerResponse(400)]
        public async Task<IActionResult> AddStation([FromBody] CreateStationDTO stationDto)
        {
            var station = _mapper.Map<Station>(stationDto);
            bool isCreated = await _stationRepository.CreateStationAsync(station);



            if (isCreated)
            {
                return Ok("Succesfully created!");
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Удалить остановку.
        /// </summary>
        [HttpDelete("{id}")]
        [SwaggerResponse(200, Type = typeof(string))]
        [SwaggerResponse(400)]
        [SwaggerResponse(404)]
        public async Task<IActionResult> DeleteStation(int id)
        {
            var station = await _stationRepository.GetStationByIdAsync(id);
            if (station == null)
            {
                return NotFound("Station not found");

            }
            var isDeleted = await _stationRepository.DeleteStationAsync(station);
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
        /// Обновить остановку.
        /// </summary>
        [HttpPut("{id}")]
        [SwaggerResponse(200, Type = typeof(CreateStationDTO))]
        [SwaggerResponse(400)]
        [SwaggerResponse(404)]
        public async Task<IActionResult> UpdateStation(int id, CreateStationDTO StationDto)
        {
            Station station = await _stationRepository.GetStationByIdAsync(id);
            if (station == null)
            {
                return NotFound("Station not found");
            }
            station.Name = StationDto.Name;
            

            var isUpdated = await _stationRepository.UpdateStationAsync(station);
            if (isUpdated)
            {
                return Ok(StationDto);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
