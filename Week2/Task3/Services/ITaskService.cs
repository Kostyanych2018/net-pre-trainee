using Task3.Entities;

namespace Task3.Services;

public interface ITaskService
{
    Task<IEnumerable<TaskEntity>> GetAllAsync();
    Task<int> AddAsync(string title, string? description);
    Task UpdateStatusAsync(int id, bool isCompleted);
    Task DeleteAsync(int id);
}