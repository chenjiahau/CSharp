﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace TodoAPI.Repositories
{
	public class SQLTokenRepository : ITokenRepository
	{
		private readonly IConfiguration configuration;

		public SQLTokenRepository(IConfiguration _configuration)
		{
			configuration = _configuration;
		}

        public string? CreateJWTToken(IdentityUser user, List<string> roles)
        {
			var claims = new List<Claim>();

			claims.Add(new Claim(ClaimTypes.Email, user.Email));

			foreach (var role in roles)
			{
				claims.Add(new Claim(ClaimTypes.Role, role));
			}

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
			var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var token = new JwtSecurityToken(
				configuration["Jwt:Issuer"],
				configuration["Jwt:Audience"],
				claims,
				expires: DateTime.Now.AddMinutes(60),
				signingCredentials: credentials
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

