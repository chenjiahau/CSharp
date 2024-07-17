using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TodoAPI.Models;
using TodoAPI.Models.DTOs;
using TodoAPI.Repositories;

namespace TodoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UserController(IUserRepository _userRepository, IMapper _mapper)
        {
            userRepository = _userRepository;
            mapper = _mapper;
        }

        /**
         * URL: /api/User/{id}
         * METHOD: GET
         */
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById(Guid id)
        {
            // Fetch from database
            var userModel = await userRepository.GetById(id);

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
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Add([FromBody] AddUserDTO addUserDto)
        {
            // Convert User DTO to User model
            var userModel = new User
            {
                Name = addUserDto.Name,
                Email = addUserDto.Email,
            };

            // Insert to database
            userModel = await userRepository.Add(userModel);

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
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> PutById([FromRoute] Guid id, [FromBody] UpdateUserDTO updateUserDTO)
        {
            // Save to database
            var userModel = new User
            {
                Name = updateUserDTO.Name,
                Email = updateUserDTO.Email
            };

            userModel = await userRepository.PutById(id, userModel);

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
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            // Save to database
            var userModel = await userRepository.DeleteById(id);

            if (userModel == null)
            {
                return NotFound();
            }

            // Convert User model to User DTO
            var userDTO = mapper.Map<UserDTO>(userModel);

            // Return User DTO
            return Ok(userDTO);
        }
    }
}