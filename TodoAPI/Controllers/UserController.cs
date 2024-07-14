using Microsoft.AspNetCore.Mvc;
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

        public UserController(TodoDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        /**
         * URL: /api/User/{id}
         * METHOD: GET
         */
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById(Guid id)
        {
            // Fetch from database
            var userModel = dbContext.Users.FirstOrDefault(x => x.Id == id);

            if (userModel == null)
            {
                return NotFound();
            }

            // Convert User model to User DTO
            var userDto = new UserDTO
            {
                Id = userModel.Id,
                Name = userModel.Name,
                Email = userModel.Email,
            };

            // Return User DTO
            return Ok(userDto);
        }

        /**
         * URL: /api/User
         * METHOD: POST
         */
        [HttpPost]
        public IActionResult Add([FromBody] AddUserDTO addUserDto)
        {
            // Convert User DTO to User model
            var userModel = new User
            {
                Name = addUserDto.Name,
                Email = addUserDto.Email,
            };

            // Insert to database
            dbContext.Users.Add(userModel);
            dbContext.SaveChanges();

            // Convert User model to User DTO
            var userDto = new UserDTO
            {
                Id = userModel.Id,
                Name = userModel.Name,
                Email = userModel.Email,
            };

            // Return User DTO
            return Ok(userDto);
        }

        /**
         * URL: /api/User/{id}
         * METHOD: PUT
         */
        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult PutById([FromRoute] Guid id, [FromBody] UpdateUserDTO updateUserDTO)
        {
            // Fetch from database
            var userModel = dbContext.Users.FirstOrDefault(x => x.Id == id);

            if (userModel == null)
            {
                return NotFound();
            }

            // Update User model
            userModel.Name = updateUserDTO.Name;
            userModel.Email = updateUserDTO.Email;

            // Save to database
            dbContext.SaveChanges();

            // Convert User model to User DTO
            var userDTO = new UserDTO
            {
                Id = userModel.Id,
                Name = userModel.Name,
                Email = userModel.Email
            };

            // Return User DTO
            return Ok(userDTO);
        }

        /**
         * URL: /api/User/{id}
         * METHOD: DELETE
         */
        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult DeleteById([FromRoute] Guid id)
        {
            // Fetch from database
            var userModel = dbContext.Users.FirstOrDefault(x => x.Id == id);

            if (userModel == null)
            {
                return NotFound();
            }

            // Save to database
            dbContext.Users.Remove(userModel);
            dbContext.SaveChanges();

            // Convert User model to User DTO
            var userDTO = new UserDTO
            {
                Id = userModel.Id,
                Name = userModel.Name,
                Email = userModel.Email
            };

            // Return User DTO
            return Ok(userDTO);
        }
    }
}

