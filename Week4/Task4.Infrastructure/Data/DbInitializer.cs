using Microsoft.EntityFrameworkCore;

using Task4.Domain.Entities;

namespace Task4.Infrastructure.Data;

public static class DbInitializer
{
    public static async Task Initialize(LibraryContext context)
    {
        await context.Database.EnsureCreatedAsync();
        if (await context.Authors.AnyAsync() || await context.Books.AnyAsync()) {
            return;
        }

        var authors = new Author[]
        {
            new()
            {
                Name = "Sarah",
                DateOfBirth = new DateTimeOffset(1978, 3, 15, 0, 0, 0, TimeSpan.Zero)
            },
            new()
            {
                Name = "Phoenix",
                DateOfBirth = new DateTimeOffset(1985, 7, 22, 0, 0, 0, TimeSpan.Zero)
            },
            new()
            {
                Name = "John",
                DateOfBirth = new DateTimeOffset(1990, 11, 8, 0, 0, 0, TimeSpan.Zero)
            }
        };

        await context.Authors.AddRangeAsync(authors);

        var books = new Book[]
        {
            new() { Title = "Silent Forest", PublishedYear = 2015, AuthorId = 1 },
            new() { Title = "Echoes of Time", PublishedYear = 2020, AuthorId = 2 },
            new() { Title = "Beyond Horizon", PublishedYear = 2018, AuthorId = 3 },
            new() { Title = "Shadow Games", PublishedYear = 2022, AuthorId = 1 },
            new() { Title = "Digital Dreams", PublishedYear = 2019, AuthorId = 2 }
        };
        
        await context.Books.AddRangeAsync(books);
        await context.SaveChangesAsync();
    }
}