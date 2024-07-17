using Microsoft.AspNetCore.Identity;

namespace TodoAPI.Repositories
{
	public interface ITokenRepository
	{
		string? CreateJWTToken(IdentityUser user, List<string> roles);
	}
}

