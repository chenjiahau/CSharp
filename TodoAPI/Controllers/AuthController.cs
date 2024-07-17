using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using TodoAPI.Models.DTOs;
using TodoAPI.Repositories;

namespace TodoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> _userManager, ITokenRepository _tokenRepository)
        {
            userManager = _userManager;
            tokenRepository = _tokenRepository;
        }

        /**
         * URL: /api/Auth/Register
         * METHOD: POST
         */
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] AuthDTO authDTO)
        {
            var identityUser = new IdentityUser
            {
                UserName = authDTO.Username,
                Email = authDTO.Username
            };

            var identityResult = await userManager.CreateAsync(identityUser, authDTO.Password);

            if (identityResult.Succeeded)
            {
                if (authDTO.Roles != null && authDTO.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, authDTO.Roles);

                    if (identityResult.Succeeded)
                    {
                        return Ok("Register successful.");
                    }
                }
            }

            return BadRequest("Something wrong!");
        }

        /**
        * URL: /api/Auth/Login
        * METHOD: POST
        */
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] AuthDTO authDTO)
        {
            var user = await userManager.FindByEmailAsync(authDTO.Username);

            if (user != null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, authDTO.Password);

                if (checkPasswordResult)
                {
                    var roles = await userManager.GetRolesAsync(user);

                    if (roles != null)
                    {
                        var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());
                        var loginResponseDTO = new LoginResponseDTO
                        {
                            JwtToken = jwtToken
                        };

                        return Ok(loginResponseDTO);
                    }
                }
            }

            return BadRequest("Username or password incorrect.");
        }
    }
}

