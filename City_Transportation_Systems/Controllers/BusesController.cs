﻿using AutoMapper;
using City_Transportation_Systems.DTO;
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
        private ILogger _logger;
        private IMapper _mapper;
        public BusesController(IBusRepository busRepository, ILogger<BusesController> logger, IMapper mapper)
        {
            _busRepository = busRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetBuses()
        {
            var buses = await _busRepository.GetAllBusesAsync();
            return Ok(_mapper.Map<IEnumerable<BusDTO>>(buses));
        }

        [HttpPost]
        public async Task<IActionResult> AddBus([FromBody] CreateBusDTO busDto)
        {
            var bus = _mapper.Map<Bus>(busDto);
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
            if (bus == null)
            {
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
            if (bus == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(_mapper.Map<BusDTO>(bus));
            }
        }


        [HttpGet("route/{id}")]
        public async Task<IActionResult> GetBusByRouteId(int RouteId)
        {
            var buses = await _busRepository.GetBusesByRouteAsync(RouteId);
            if (buses == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(buses);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBus(int id, CreateBusDTO busDto)
        {
            Bus bus = await _busRepository.GetBusByIdAsync(id);
            if (bus == null)
            {
                return NotFound();
            }

            bus.Capacity = busDto.Capacity;
            bus.Model = busDto.Model;
            bus.RouteId = busDto.RouteId;

            var isUpdated = await _busRepository.UpdateBusAsync(bus);
            if (isUpdated)
            {
                return Ok(busDto);
            }
            else
            {
                return BadRequest();
            }
        }
    }
    }

