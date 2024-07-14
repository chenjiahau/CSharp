using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using TodoAPI.Data;
using TodoAPI.Models.DTOs;

namespace TodoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulesController : Controller
    {
        private readonly TodoDbContext dbContext;
        private readonly IMapper mapper;

        public SchedulesController(TodoDbContext _dbContext, IMapper _mapper)
        {
            dbContext = _dbContext;
            mapper = _mapper;
        }

        /**
        * URL: /api/Schedules
        * METHOD: GET
        */
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Fetch from database
            var scheduleModels = await dbContext.Schedules.Include("User").Include("Work").ToListAsync();

            // Convert Schedule models to Schedule DTOs
            var scheduleDTOs = mapper.Map<List<ScheduleDTO>>(scheduleModels);

            // Return Schedule DTOs
            return Ok(scheduleDTOs);
        }
    }
}

