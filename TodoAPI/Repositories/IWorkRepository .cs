using TodoAPI.Models;

namespace TodoAPI.Repositories
{
	public interface IWorkRepository
	{
        Task<List<Work>> GetAll(
            string? column,
            string? keyword,
            string? sortBy,
            bool isAsc,
            int pageNumber,
            int pageSize
        );
        Task<Work?> GetById(Guid id);
        Task<Work?> Add(Work work);
        Task<Work?> PutById(Guid id, Work work);
        Task<Work?> DeleteById(Guid id);
    }
}