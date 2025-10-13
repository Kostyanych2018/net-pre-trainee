using Task3.Entities;

namespace Task3.Repositories;

public interface ITaskRepository
{
    Task<IEnumerable<TaskEntity>> GetAllAsync();
    Task<int> AddAsync(TaskEntity taskEntity);
    Task<bool> UpdateStatusAsync(int id, bool isCompleted);
    Task<bool> DeleteAsync(int id);
}