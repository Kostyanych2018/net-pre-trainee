using Task3.Entities;
using Task3.Repositories;

namespace Task3;

public class MenuOptions
{
    private readonly ITaskRepository _taskRepository;

    public MenuOptions(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public string? GetUserChoice()
    {
        Console.WriteLine("TaskEntity management menu");
        Console.WriteLine("1. List all tasks");
        Console.WriteLine("2. Add a new task");
        Console.WriteLine("3. Update a task's status");
        Console.WriteLine("4. Delete a task");
        Console.WriteLine("5. Exit");
        Console.Write("Select an option: ");
        return Console.ReadLine();
    }

    public async Task ListAllTasks()
    {
        Console.WriteLine("\nFetching tasks from the database...");
        var tasks = (await _taskRepository.GetAllAsync()).ToList();

        if (!tasks.Any()) {
            Console.WriteLine("No tasks found in the database.");
            return;
        }

        Console.WriteLine("Current tasks:");
        foreach (var task in tasks) {
            var status = task.IsCompleted ? "Completed" : "Uncompleted";
            var output = $"  ID: {task.Id}, Title: {task.Title}," +
                         $" Status: {status}, Created: {task.CreatedAt: yyyy-MM-dd HH:mm:ss.fff}";
            if (!string.IsNullOrEmpty(task.Description)) {
                output += $", Description: {task.Description}";
            }

            Console.WriteLine(output);
        }
    }

    public async Task AddNewTask()
    {
        Console.WriteLine("\nAdd New TaskEntity ");
        Console.Write("Enter Title: ");
        string? title = Console.ReadLine();

        if (string.IsNullOrEmpty(title)) {
            Console.WriteLine("Title is required.");
            return;
        }

        const int maxTitleLength = 255;
        if (title.Length > maxTitleLength) {
            Console.WriteLine($"Title is too long. Maximum {maxTitleLength} characters allowed.");
            Console.WriteLine($"Current length: {title.Length} characters.");
            return;
        }

        Console.Write("Enter Description: ");
        string? description = Console.ReadLine();

        var newTask = new TaskEntity()
        {
            Title = title,
            Description = description,
            IsCompleted = false,
            CreatedAt = DateTime.Now
        };

        int newId = await _taskRepository.AddAsync(newTask);
        Console.WriteLine($"Successfully added new task with ID: {newId}");
    }

    public async Task UpdateTaskStatus()
    {
        Console.WriteLine("\nUpdate TaskEntity Status");
        Console.Write("Enter the task ID to update: ");
        if (!int.TryParse(Console.ReadLine(), out int id) || id <= 0) {
            Console.WriteLine("Invalid ID format. Please, enter a positive number.");
            return;
        }

        Console.WriteLine("  y - Mark task as COMPLETED");
        Console.WriteLine("  n - Mark task as UNCOMPLETED");
        Console.Write("Enter your choice (y/n): ");
        string? choice = Console.ReadLine()?.ToLower();

        bool isCompleted;
        if (choice == "y") {
            isCompleted = true;
        }
        else if (choice == "n") {
            isCompleted = false;
        }
        else {
            Console.WriteLine("Wrong choice. Please enter 'y' or 'n'.");
            return;
        }

        bool success = await _taskRepository.UpdateStatusAsync(id, isCompleted);
        if (success) {
            Console.WriteLine($"Status of the task with ID: {id} has been successfully updated.");
        }
        else {
            Console.WriteLine($"Failed to update status of the task with ID: {id}. It might not exist.");
        }
    }

    public async Task DeleteTask()
    {
        Console.WriteLine("\nDelete TaskEntity");
        Console.Write("Enter the task ID to delete: ");
        if (!int.TryParse(Console.ReadLine(), out int id) || id <= 0) {
            Console.WriteLine("Invalid ID format. Please, enter a positive number.");
            return;
        }

        bool success = await _taskRepository.DeleteAsync(id);
        if (success) {
            Console.WriteLine($"Successfully deleted task with ID: {id}");
        }
        else {
            Console.WriteLine($"Failed to delete task with ID: {id}. It might not exist.");
        }
    }
}