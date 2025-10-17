using Microsoft.Extensions.Configuration;
using Task3.Entities;
using Task3.Enums;
using Task3.Factories;
using Task3.Repositories;
using Task3.Services;

namespace Task3;

internal class Program
{
    static async Task Main(string[] args)
    {
        var builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true)
            .AddUserSecrets<Program>();

        IConfiguration config = builder.Build();
        string connectionString = config.GetConnectionString("DefaultConnection");

        IDbConnectionFactory connectionFactory = new SqlConnectionFactory(connectionString);
        ITaskRepository taskRepository = new TaskRepository(connectionFactory);
        ITaskService taskService = new TaskService(taskRepository);
        MenuOptions menuOptions = new MenuOptions(taskService);

        while (true) {
            var choice = menuOptions.GetUserChoice();

            MenuOption option = MenuOption.Invalid;
            if (int.TryParse(choice, out int numericChoice)) {
                if (Enum.IsDefined(typeof(MenuOption), numericChoice)) {
                    option = (MenuOption)numericChoice;
                }
            }

            try {
                switch (option) {
                    case MenuOption.ListAllTasks:
                        await menuOptions.ListAllTasks();
                        break;
                    case MenuOption.AddNewTask:
                        await menuOptions.AddNewTask();
                        break;
                    case MenuOption.UpdateTaskStatus:
                        await menuOptions.UpdateTaskStatus();
                        break;
                    case MenuOption.DeleteTask:
                        await menuOptions.DeleteTask();
                        break;
                    case MenuOption.Exit:
                        Console.WriteLine("Exiting application.");
                        return;
                    case MenuOption.Invalid:
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