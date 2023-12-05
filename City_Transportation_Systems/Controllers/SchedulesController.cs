using AutoMapper;
using City_Transportation_Systems.DTO;
using City_Transportation_Systems.Interfaces;
using City_Transportation_Systems.Models;
using City_Transportation_Systems.Repository;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace City_Transportation_Systems.Controllers
{
    [Route("api/schedules")]
    [ApiController]
    public class SchedulesController : ControllerBase

    {
        private IScheduleRepository _scheduleRepository;
        private ILogger _logger;
        private IMapper _mapper;

        public SchedulesController(IScheduleRepository scheduleRepository, ILogger<SchedulesController> logger, IMapper mapper)
        {
            _scheduleRepository = scheduleRepository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить полное расписание.
        /// </summary>
        [HttpGet]
        [SwaggerResponse(200, Type = typeof(List<ScheduleDTO>))]
        [SwaggerResponse(404, Type = typeof(string))]
        public async Task<IActionResult> GetStations()
        {
            var schedule = await _scheduleRepository.GetFullScheduleAsync();
            return Ok(_mapper.Map<List<ScheduleDTO>>(schedule));
        }


        /// <summary>
        /// Получить расписание по айди.
        /// </summary>
        [HttpGet("{id}")]
        [SwaggerResponse(200, Type = typeof(ScheduleDTO))]
        [SwaggerResponse(404, Type = typeof(string))]
        public async Task<IActionResult> GetScheduleById(int id)
        {
            var schedule = await _scheduleRepository.GetScheduleByIdAsync(id);
            if (schedule == null)
            {
                return NotFound("Schedule not found");
            }
            else
            {
                return Ok(_mapper.Map<ScheduleDTO>(schedule));
            }
        }

        /// <summary>
        /// Получить расписание по айди остановки.
        /// </summary>
        [HttpGet("station/{StationId}")]
        [SwaggerResponse(200, Type = typeof(List<ScheduleDTO>))]
        [SwaggerResponse(404, Type = typeof(string))]
        public async Task<IActionResult> GetScheduleByStationId(int StationId)
        {
            var schedules = await _scheduleRepository.GetSchedulesByStationAsync(StationId);
            if (schedules == null)
            {
                return NotFound("Schedules not found");
            }
            else
            {
                return Ok(schedules);
            }
        }

        /// <summary>
        /// Получить расписание по айди маршрута.
        /// </summary>
        [HttpGet("route/{RouteId}")]
        [SwaggerResponse(200, Type = typeof(List<ScheduleDTO>))]
        [SwaggerResponse(404, Type = typeof(string))]
        public async Task<IActionResult> GetScheduleByRoute(int RouteId)
        {
            var schedules = await _scheduleRepository.GetSchedulesByRouteAsync(RouteId);
            if (schedules == null)
            {
                return NotFound("Schedules not found");
            }
            else
            {
                return Ok(schedules);
            }
        }

        /// <summary>
        /// Добавить расписание.
        /// </summary>
        [HttpPost]
        [SwaggerResponse(200, Type = typeof(string))]
        [SwaggerResponse(400)]
        public async Task<IActionResult> AddSchedule([FromBody] CreateScheduleDTO scheduleDto)
        {
            if (!ModelState.IsValid) {
                return BadRequest();
            }
            var schedule = _mapper.Map<Schedule>(scheduleDto);
            bool isCreated = await _scheduleRepository.CreateScheduleAsync(schedule);

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
        /// Удалить расписание.
        /// </summary>
        [HttpDelete("{id}")]
        [SwaggerResponse(200, Type = typeof(string))]
        [SwaggerResponse(400)]
        [SwaggerResponse(404)]
        public async Task<IActionResult> DeleteSchedule(int id)
        {
            var schedule = await _scheduleRepository.GetScheduleByIdAsync(id);
            if (schedule == null)
            {
                return NotFound("Station not found");

            }
            var isDeleted = await _scheduleRepository.DeleteScheduleAsync(schedule);
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
        /// Обновить расписание по айди.
        /// </summary>
        [HttpPut("{id}")]
        [SwaggerResponse(200, Type = typeof(CreateStationDTO))]
        [SwaggerResponse(400)]
        [SwaggerResponse(404)]
        public async Task<IActionResult> UpdateSchedule(int id, CreateScheduleDTO ScheduleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Schedule schedule = await _scheduleRepository.GetScheduleByIdAsync(id);
            if (schedule == null)
            {
                return NotFound("Station not found");
            }
            schedule.TimeStamp = ScheduleDto.TimeStamp;
            schedule.RouteId = ScheduleDto.RouteId;
            schedule.StationId = ScheduleDto.StationId;


            var isUpdated = await _scheduleRepository.UpdateScheduleAsync(schedule);
            if (isUpdated)
            {
                return Ok(ScheduleDto);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
