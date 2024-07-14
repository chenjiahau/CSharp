using AutoMapper;
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
        public async Task<IActionResult> GetAll()
        {
            // Fetch from database
            var userModels = await userRepository.GetAll();

            // Convert User models to User DTOs
            var userDTOs = mapper.Map<List<UserDTO>>(userModels);

            // Return User DTOs
            return Ok(userDTOs);
        }
    }
}