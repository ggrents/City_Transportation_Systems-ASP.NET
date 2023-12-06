using AutoMapper;
using City_Transportation_Systems.DTO;
using City_Transportation_Systems.Interfaces;
using City_Transportation_Systems.Models;
using City_Transportation_Systems.Repository;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace City_Transportation_Systems.Controllers
{

    [ApiController]
    [Route("api/drivers")]
    public class DriversController : Controller
    {
        private IDriverRepository _driverRepository;
        private ILogger _logger;
        private IMapper _mapper;

        public DriversController(IDriverRepository driverRepository,ILogger<DriversController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _driverRepository = driverRepository;
        }

        /// <summary>
        /// Получить список водителей.
        /// </summary>
        [HttpGet]
        [SwaggerResponse(200, Type = typeof(List<DriverDTO>))]
        public async Task<IActionResult> GetDrivers()
        {
            var drivers = await _driverRepository.GetAllDriversAsync();
            return Ok(_mapper.Map<IEnumerable<DriverDTO>>(drivers));
        }

        /// <summary>
        /// Добавить водителя.
        /// </summary>
        [HttpPost]
        [SwaggerResponse(200, Type = typeof(string))]
        [SwaggerResponse(400)]
        public async Task<IActionResult> AddDriver([FromBody] CreateDriverDTO driverDto)
        {
            var driver = _mapper.Map<Driver>(driverDto);
            bool isCreated = await _driverRepository.CreateDriverAsync(driver);

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
        /// Удалить водителя.
        /// </summary>
        [HttpDelete("{id}")]
        [SwaggerResponse(200, Type = typeof(string))]
        [SwaggerResponse(400, Type = typeof(string))]
        [SwaggerResponse(404, Type = typeof(string))]
        public async Task<IActionResult> DeleteDriver(int id)
        {
            Driver driver = await _driverRepository.GetDriverByIdAsync(id);
            if (driver == null)
            {
                return NotFound("Driver not found");

            }
            var isDeleted = await _driverRepository.DeleteDriverAsync(driver);
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
        /// Получить водителя по айди.
        /// </summary>
        [HttpGet("{id}")]
        [SwaggerResponse(200, Type = typeof(DriverDTO))]
        [SwaggerResponse(404, Type = typeof(string))]
        public async Task<IActionResult> GetDriverById(int id)
        {
            var driver = await _driverRepository.GetDriverByIdAsync(id);
            if (driver == null)
            {
                return NotFound("Driver not found");
            }
            else
            {
                return Ok(_mapper.Map<DriverDTO>(driver));
            }
        }


        /// <summary>
        /// Обновить водителя.
        /// </summary>
        [HttpPut("{id}")]
        [SwaggerResponse(200, Type = typeof(CreateDriverDTO))]
        [SwaggerResponse(404, Type = typeof(string))]
        [SwaggerResponse(400)]
        public async Task<IActionResult> UpdateDriver(int id, CreateDriverDTO DriverDto)
        {
            Driver driver = await _driverRepository.GetDriverByIdAsync(id);
            if (driver == null)
            {
                return NotFound("Driver not found");
            }
            driver.First_name = DriverDto.First_name;
            driver.Last_name = DriverDto.Last_name;
            driver.BusId = DriverDto.BusId;

            var isUpdated = await _driverRepository.UpdateDriverAsync(driver);
            if (isUpdated)
            {
                return Ok(DriverDto);
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
