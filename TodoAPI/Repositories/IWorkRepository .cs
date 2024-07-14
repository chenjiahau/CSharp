using TodoAPI.Models;

namespace TodoAPI.Repositories
{
	public interface IWorkRepository
	{
        Task<List<Work>> GetAll();
        Task<Work?> GetById(Guid id);
        Task<Work?> Add(Work work);
        Task<Work?> PutById(Guid id, Work work);
        Task<Work?> DeleteById(Guid id);
    }
}