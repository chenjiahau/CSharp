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

        public SchedulesController(TodoDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        /**
        * URL: /api/Schedules
        * METHOD: GET
        */
        [HttpGet]
        public IActionResult GetAll()
        {
            // Fetch from database
            var scheduleModels = dbContext.Schedules.Include("User").Include("Work").ToList();

            // Convert Schedule models to Schedule DTOs
            var scheduleDTOs = new List<ScheduleDTO>();
            foreach (var scheduleModel in scheduleModels)
            {
                var userDTO = new UserDTO
                {
                    Id = scheduleModel.UserId,
                    Name = scheduleModel.User?.Name,
                    Email = scheduleModel.User?.Email
                };

                var workDTO = new WorkDTO
                {
                    Id = scheduleModel.WorkId,
                    Title = scheduleModel.Work?.Title,
                    Description = scheduleModel.Work?.Description
                };

                scheduleDTOs.Add(new ScheduleDTO
                {
                    Id = scheduleModel.Id,
                    ExectionDate = scheduleModel.ExectionDate,
                    User = userDTO,
                    Work = workDTO
                });
            }

            // Return Schedule DTOs
            return Ok(scheduleDTOs);
        }
    }
}

