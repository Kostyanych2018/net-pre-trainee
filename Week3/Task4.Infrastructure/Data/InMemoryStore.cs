using Task4.Domain.Entities;

namespace Task4.Infrastructure.Data;

public static class InMemoryStore
{
    public static List<Author> Authors =
    [
        new Author() { Id = 1, Name = "Sarah", DateOfBirth = new DateTime(1978, 3, 15) },
        new Author() { Id = 2, Name = "Phoenix", DateOfBirth = new DateTime(1985, 7, 22) },
        new Author() { Id = 3, Name = "John", DateOfBirth = new DateTime(1990, 11, 8) }
    ];

    public static List<Book> Books =
    [
        new Book() { Id = 1, Title = "Silent Forest", PublishedYear = 2015, AuthorId = 1 },
        new Book() { Id = 2, Title = "Echoes of Time", PublishedYear = 2020, AuthorId = 2 },
        new Book() { Id = 3, Title = "Beyond Horizon", PublishedYear = 2018, AuthorId = 3 },
        new Book() { Id = 4, Title = "Shadow Games", PublishedYear = 2022, AuthorId = 1 },
        new Book() { Id = 5, Title = "Digital Dreams", PublishedYear = 2019, AuthorId = 2 }
    ];
}