using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using TodoAPI.Data;
using TodoAPI.Models.DTOs;

namespace TodoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorksController : ControllerBase
    {
        private readonly TodoDbContext dbContext;
        private readonly IMapper mapper;

        public WorksController(TodoDbContext _dbContext, IMapper _mapper)
        {
            dbContext = _dbContext;
            mapper = _mapper;
        }

        /**
         * URL: /api/Works
         * METHOD: GET
         */
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Fetch from database
            var workModels = await dbContext.Works.ToListAsync();

            // Convert Work models to Work DTOs
            var workDTOs = mapper.Map<List<WorkDTO>>(workModels);

            // Return Work DTOs
            return Ok(workDTOs);
        }
    }
}

