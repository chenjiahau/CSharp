using AutoMapper;
using Microsoft.AspNetCore.Mvc;

using TodoAPI.Models.DTOs;
using TodoAPI.Repositories;

namespace TodoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulesController : Controller
    {
        private readonly IScheduleRepository scheduleRepository;
        private readonly IMapper mapper;

        public SchedulesController(IScheduleRepository _scheduleRepository, IMapper _mapper)
        {
            scheduleRepository = _scheduleRepository;
            mapper = _mapper;
        }

        /**
        * URL: /api/Schedules
        * METHOD: GET
        */
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? column,
            [FromQuery] string? keyword,
            [FromQuery] string? sortBy,
            [FromQuery] bool isAsc,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 1
        ) {
            // Fetch from database
            var scheduleModels = await scheduleRepository.GetAll(column, keyword, sortBy, isAsc, pageNumber, pageSize);

            // Convert Schedule models to Schedule DTOs
            var scheduleDTOs = mapper.Map<List<ScheduleDTO>>(scheduleModels);

            // Return Schedule DTOs
            return Ok(scheduleDTOs);
        }
    }
}