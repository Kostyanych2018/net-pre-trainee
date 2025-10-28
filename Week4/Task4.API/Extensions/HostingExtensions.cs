using Task4.Infrastructure.Data;

namespace Task4.API.Extensions;

public static class HostingExtensions
{
    public static async Task SeedDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;

        try {
            var context = services.GetRequiredService<LibraryContext>();
            await DbInitializer.Initialize(context);
        }
        catch (Exception ex) {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred while seeding the database.");
        }
    }
}