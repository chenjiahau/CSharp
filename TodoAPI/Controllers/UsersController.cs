using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TodoAPI.Models.DTOs;
using TodoAPI.Repositories;

namespace TodoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UsersController(IUserRepository _userRepository, IMapper _mapper)
        {
            userRepository = _userRepository;
            mapper = _mapper;
        }

        /**
         * URL: /api/Users
         * METHOD: GET
         */
        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? column,
            [FromQuery] string? keyword,
            [FromQuery] string? sortBy,
            [FromQuery] bool isAsc,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 1
        ) {
            // Fetch from database
            var userModels = await userRepository.GetAll(column, keyword, sortBy, isAsc, pageNumber, pageSize);

            // Convert User models to User DTOs
            var userDTOs = mapper.Map<List<UserDTO>>(userModels);

            // Return User DTOs
            return Ok(userDTOs);
        }
    }
}