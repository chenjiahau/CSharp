using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using TodoAPI.Data;
using TodoAPI.Models;
using TodoAPI.Models.DTOs;

namespace TodoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly TodoDbContext dbContext;
        private readonly IMapper mapper;

        public UserController(TodoDbContext _dbContext, IMapper _mapper)
        {
            dbContext = _dbContext;
            mapper = _mapper;
        }

        /**
         * URL: /api/User/{id}
         * METHOD: GET
         */
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            // Fetch from database
            var userModel = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (userModel == null)
            {
                return NotFound();
            }

            // Convert User model to User DTO
            var userDto = mapper.Map<UserDTO>(userModel);

            // Return User DTO
            return Ok(userDto);
        }

        /**
         * URL: /api/User
         * METHOD: POST
         */
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddUserDTO addUserDto)
        {
            // Convert User DTO to User model
            var userModel = new User
            {
                Name = addUserDto.Name,
                Email = addUserDto.Email,
            };

            // Insert to database
            await dbContext.Users.AddAsync(userModel);
            await dbContext.SaveChangesAsync();

            // Convert User model to User DTO
            var userDto = mapper.Map<UserDTO>(userModel);

            // Return User DTO
            return Ok(userDto);
        }

        /**
         * URL: /api/User/{id}
         * METHOD: PUT
         */
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> PutById([FromRoute] Guid id, [FromBody] UpdateUserDTO updateUserDTO)
        {
            // Fetch from database
            var userModel = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (userModel == null)
            {
                return NotFound();
            }

            // Update User model
            userModel.Name = updateUserDTO.Name;
            userModel.Email = updateUserDTO.Email;

            // Save to database
            await dbContext.SaveChangesAsync();

            // Convert User model to User DTO
            var userDTO = mapper.Map<UserDTO>(userModel);

            // Return User DTO
            return Ok(userDTO);
        }

        /**
         * URL: /api/User/{id}
         * METHOD: DELETE
         */
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            // Fetch from database
            var userModel = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (userModel == null)
            {
                return NotFound();
            }

            // Save to database
            dbContext.Users.Remove(userModel);
            await dbContext.SaveChangesAsync();

            // Convert User model to User DTO
            var userDTO = mapper.Map<UserDTO>(userModel);

            // Return User DTO
            return Ok(userDTO);
        }
    }
}

