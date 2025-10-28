using Task4.Application.DTOs;
using Task4.Domain.Entities;

namespace Task4.Application.Mappers;

public static class BookMapper
{
    public static BookDto ToDto(Book book) => new()
    {
        Id = book.Id,
        Title = book.Title,
        PublishedYear = book.PublishedYear,
        AuthorId = book.AuthorId,
    };

    public static Book ToEntity(CreateBookDto book) => new()
    {
        Title = book.Title,
        PublishedYear = book.PublishedYear,
        AuthorId = book.AuthorId,
    };
}