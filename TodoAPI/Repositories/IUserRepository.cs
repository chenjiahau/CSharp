using TodoAPI.Models;

namespace TodoAPI.Repositories
{
	public interface IUserRepository
	{
        Task<List<User>> GetAll(string? column, string? keyword);
        Task<User?> GetById(Guid id);
        Task<User?> Add(User user);
        Task<User?> PutById(Guid id, User user);
        Task<User?> DeleteById(Guid id);
    }
}