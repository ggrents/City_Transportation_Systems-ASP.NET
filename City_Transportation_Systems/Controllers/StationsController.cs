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

        [HttpGet]
        [SwaggerOperation(Summary = "Получить список остановок")]
        [SwaggerResponse(200, Type = typeof(List<StationDTO>))]
        [SwaggerResponse(404, Type = typeof(string))]
        public async Task<IActionResult> GetStations()
        {
            var stations = await _stationRepository.GetAllStationsAsync();
            return Ok(_mapper.Map<List<StationDTO>>(stations));
        }

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
