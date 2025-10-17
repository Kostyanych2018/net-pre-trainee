using Task3.Entities;
using Task3.Repositories;

namespace Task3.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;
    private const int MaxTitleLength = 255;
    
    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }
    
    public async Task<IEnumerable<TaskEntity>> GetAllAsync()
    {
        return await _taskRepository.GetAllAsync();
    }

    public async Task<int> AddAsync(string title, string? description)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title is required.", nameof(title));
        }
        
        if (title.Length > MaxTitleLength)
        {
            throw new ArgumentException($"Title is too long. Maximum {MaxTitleLength} characters allowed.", nameof(title));
        }
        
        var newTask = new TaskEntity
        {
            Title = title,
            Description = description,
            IsCompleted = false,
            CreatedAt = DateTime.Now
        };

        return await _taskRepository.AddAsync(newTask);
    }

    public async Task UpdateStatusAsync(int id, bool isCompleted)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Invalid task ID.", nameof(id));
        }

        bool success = await _taskRepository.UpdateStatusAsync(id, isCompleted);
        if (!success)
        {
            throw new KeyNotFoundException($"Task with ID: {id} not found.");
        }
    }

    public async Task DeleteAsync(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Invalid task ID.", nameof(id));
        }

        bool success = await _taskRepository.DeleteAsync(id);
        if (!success)
        {
            throw new KeyNotFoundException($"Task with ID: {id} not found.");
        }
    }
}