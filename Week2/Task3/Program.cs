using Microsoft.Extensions.Configuration;
using Task3.Entities;
using Task3.Factories;
using Task3.Repositories;

namespace Task3;

internal class Program
{
    private static ITaskRepository _taskRepository;

    static async Task Main(string[] args)
    {
        var builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true)
            .AddUserSecrets<Program>();

        IConfiguration config = builder.Build();
        string connectionString = config.GetConnectionString("DefaultConnection");

        IDbConnectionFactory connectionFactory = new SqlConnectionFactory(connectionString);
        _taskRepository = new TaskRepository(connectionFactory);
        MenuOptions menuOptions = new MenuOptions(_taskRepository);

        while (true) {
            var choice = menuOptions.GetUserChoice();

            try {
                switch (choice) {
                    case "1":
                        await menuOptions.ListAllTasks();
                        break;
                    case "2":
                        await menuOptions.AddNewTask();
                        break;
                    case "3":
                        await menuOptions.UpdateTaskStatus();
                        break;
                    case "4":
                        await menuOptions.DeleteTask();
                        break;
                    case "5":
                        Console.WriteLine("Exiting application.");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
            catch (Exception ex) {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}