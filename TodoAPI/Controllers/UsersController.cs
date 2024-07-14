using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using TodoAPI.Data;
using TodoAPI.Models.DTOs;

namespace TodoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly TodoDbContext dbContext;

        public UsersController(TodoDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        /**
         * URL: /api/Users
         * METHOD: GET
         */
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Fetch from database
            var userModels = await dbContext.Users.ToListAsync();

            // Convert User models to User DTOs
            var userDTOs = new List<UserDTO>();
            foreach (var userModel in userModels)
            {
                userDTOs.Add(
                    new UserDTO()
                    {
                        Id = userModel.Id,
                        Name = userModel.Name,
                        Email = userModel.Email,
                    }
                );
            }

            // Return User DTOs
            return Ok(userDTOs);
        }
    }
}

