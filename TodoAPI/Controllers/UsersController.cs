using AutoMapper;
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
        private readonly IMapper mapper;

        public UsersController(TodoDbContext _dbContext, IMapper _mapper)
        {
            dbContext = _dbContext;
            mapper = _mapper;
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
            var userDTOs = mapper.Map<List<UserDTO>>(userModels);

            // Return User DTOs
            return Ok(userDTOs);
        }
    }
}

