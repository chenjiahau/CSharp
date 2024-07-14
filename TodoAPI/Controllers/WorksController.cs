using Microsoft.AspNetCore.Mvc;
using TodoAPI.Data;
using TodoAPI.Models.DTOs;

namespace TodoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorksController : ControllerBase
    {
        private readonly TodoDbContext dbContext;

        public WorksController(TodoDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        /**
         * URL: /api/Works
         * METHOD: GET
         */
        [HttpGet]
        public IActionResult GetAll()
        {
            // Fetch from database
            var workModels = dbContext.Works.ToList();

            // Convert Work models to Work DTOs
            var workDTOs = new List<WorkDTO>();
            foreach (var workModel in workModels)
            {
                workDTOs.Add(
                    new WorkDTO()
                    {
                        Id = workModel.Id,
                        Title = workModel.Title,
                        Description = workModel.Description
                    }
                );
            }

            // Return Work DTOs
            return Ok(workDTOs);
        }
    }
}

