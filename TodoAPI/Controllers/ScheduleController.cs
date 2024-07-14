using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using TodoAPI.Data;
using TodoAPI.Models;
using TodoAPI.Models.DTOs;

namespace TodoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : Controller
    {
        private readonly TodoDbContext dbContext;

        public ScheduleController(TodoDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        /**
         * URL: /api/Schedule/{id}
         * METHOD: GET
         */
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            // Fetch from database
            var scheduleModel = await dbContext.Schedules.Include("User").Include("Work").FirstOrDefaultAsync(x => x.Id == id);

            if (scheduleModel == null)
            {
                return NotFound();
            }

            // Convert Schedule model to Schedule DTO
            var scheduleDTO = new ScheduleDTO
            {
                Id = scheduleModel.Id,
                ExectionDate = scheduleModel.ExectionDate,
                User = new UserDTO
                {
                    Id = scheduleModel.UserId,
                    Name = scheduleModel.User?.Name,
                    Email = scheduleModel.User?.Email
                },
                Work = new WorkDTO
                {
                    Id = scheduleModel.WorkId,
                    Title = scheduleModel.Work?.Title,
                    Description = scheduleModel.Work?.Description
                }
            };

            // Return Schedule DTO
            return Ok(scheduleDTO);
        }

        /**
         * URL: /api/Schedule
         * METHOD: POST
         */
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddScheduleDTO addScheduleDTO)
        {
            // Convert Schedule DTO to Schedule model
            var scheduleModel = new Schedule
            {
                ExectionDate = addScheduleDTO.ExectionDate,
                UserId = addScheduleDTO.UserId,
                WorkId = addScheduleDTO.WorkId
            };

            // Insert to database
            await dbContext.Schedules.AddAsync(scheduleModel);
            await dbContext.SaveChangesAsync();

            // Fetch from database and convert User model to User DTO
            var userModel = dbContext.Users.FirstOrDefault(x => x.Id == scheduleModel.UserId);
            var userDTO = new UserDTO { };
            if (userModel != null)
            {
                userDTO = new UserDTO
                {
                    Id = scheduleModel.UserId,
                    Name = userModel.Name,
                    Email = userModel.Email
                };
            }

            // Fetch from database and convert Work model to Work DTO
            var workModel = await dbContext.Works.FirstOrDefaultAsync(x => x.Id == scheduleModel.WorkId);
            var workDTO = new WorkDTO { };
            if (workModel != null)
            {
                workDTO = new WorkDTO
                {
                    Id = scheduleModel.WorkId,
                    Title = workModel.Title,
                    Description = workModel.Description
                };
            }

            // Create Schedule DTO
            var scheduleDTO = new ScheduleDTO
            {
                Id = scheduleModel.Id,
                User = userDTO,
                Work = workDTO
            };

            // Return Schedule DTO
            return Ok(scheduleDTO);
        }

        /**
         * URL: /api/Schedule/{id}
         * METHOD: PUT
         */
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> PutById([FromRoute] Guid id, [FromBody] UpdateScheduleDTO updateScheduleDTO)
        {
            // Fetch from database
            var scheduleModel = await dbContext.Schedules.Include("User").Include("Work").FirstOrDefaultAsync(x => x.Id == id);

            if (scheduleModel == null)
            {
                return NotFound();
            }

            // Update Schedule model
            scheduleModel.ExectionDate = updateScheduleDTO.ExectionDate;
            scheduleModel.UserId = updateScheduleDTO.UserId;
            scheduleModel.WorkId = updateScheduleDTO.WorkId;

            // Save to database
            await dbContext.SaveChangesAsync();

            // Convert Schedule model to Schedule DTO
            var newScheduleModel = await dbContext.Schedules.Include("User").Include("Work").FirstOrDefaultAsync(x => x.Id == id);
            var scheduleDTO = new ScheduleDTO { };
            if (newScheduleModel != null)
            {
                scheduleDTO = new ScheduleDTO
                {
                    Id = newScheduleModel.Id,
                    ExectionDate = newScheduleModel.ExectionDate,
                    User = new UserDTO
                    {
                        Id = newScheduleModel.UserId,
                        Name = newScheduleModel.User?.Name,
                        Email = newScheduleModel.User?.Email
                    },
                    Work = new WorkDTO
                    {
                        Id = newScheduleModel.WorkId,
                        Title = newScheduleModel.Work?.Title,
                        Description = newScheduleModel.Work?.Description
                    }
                };
            }

            // Return Schedule DTO
            return Ok(scheduleDTO);
        }

        /**
         * URL: /api/Schedule/{id}
         * METHOD: DELETE
         */
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            // Fetch from database
            var scheduleModel = await dbContext.Schedules.Include("User").Include("Work").FirstOrDefaultAsync(x => x.Id == id);

            if (scheduleModel == null)
            {
                return NotFound();
            }

            // Save to database
            dbContext.Schedules.Remove(scheduleModel);
            await dbContext.SaveChangesAsync();

            // Convert Schedule model to Schedule DTO
            var scheduleDTO = new ScheduleDTO
            {
                Id = scheduleModel.Id,
                ExectionDate = scheduleModel.ExectionDate,
                User = new UserDTO
                {
                    Id = scheduleModel.UserId,
                    Name = scheduleModel.User?.Name,
                    Email = scheduleModel.User?.Email
                },
                Work = new WorkDTO
                {
                    Id = scheduleModel.WorkId,
                    Title = scheduleModel.Work?.Title,
                    Description = scheduleModel.Work?.Description
                }
            };

            // Return Schedule DTO
            return Ok(scheduleDTO);
        }
    }
}

