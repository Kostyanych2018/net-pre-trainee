using Task4.Application.DTOs;
using Task4.Domain.Entities;

namespace Task4.Application.Mappers;

public static class AuthorMapper
{
    public static AuthorDto ToDto(Author author) => new()
    {
        Id = author.Id,
        Name = author.Name,
        DateOfBirth = author.DateOfBirth,
        Books = author.Books.Select(BookMapper.ToDto).ToList()
    };

    public static Author ToEntity(CreateAuthorDto author) => new()
    {
        Name = author.Name,
        DateOfBirth = author.DateOfBirth
    };
}