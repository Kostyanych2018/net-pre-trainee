using Task3.Entities;
using Task3.Services;
using Task3.Utils;

namespace Task3;

public class MenuOptions
{
    private readonly ITaskService _taskService;

    public MenuOptions(ITaskService taskService)
    {
        _taskService = taskService;
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
        var tasks = (await _taskService.GetAllAsync()).ToList();

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
        Console.WriteLine("\nAdd new task");

        string title = ConsoleUtils.GetRequiredString("Enter task title: ", 255);
        string? description = ConsoleUtils.GetOptionalString("Enter task description (optional): ", 1000);

        try {
            int newId = await _taskService.AddAsync(title, description);
            Console.WriteLine($"Successfully added new task with ID: {newId}");
        }
        catch (ArgumentException ex) {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public async Task UpdateTaskStatus()
    {
        Console.WriteLine("\nUpdate task status");
        int id = ConsoleUtils.GetPositiveInt("Enter the task ID to update: ");

        Console.WriteLine("  y - Mark task as COMPLETED");
        Console.WriteLine("  n - Mark task as UNCOMPLETED");
        bool isCompleted = ConsoleUtils.GetYesNoChoice("Enter your choice (y/n): ");

        try {
            await _taskService.UpdateStatusAsync(id, isCompleted);
            Console.WriteLine($"Status of the task with ID: {id} has been successfully updated.");
        }
        catch (Exception ex) when (ex is KeyNotFoundException || ex is ArgumentException) {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public async Task DeleteTask()
    {
        Console.WriteLine("\nDelete task");
        int id = ConsoleUtils.GetPositiveInt("Enter the task ID to delete: ");

        try {
            await _taskService.DeleteAsync(id);
            Console.WriteLine($"Successfully deleted task with ID: {id}");
        }
        catch (Exception ex) when (ex is KeyNotFoundException || ex is ArgumentException) {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}