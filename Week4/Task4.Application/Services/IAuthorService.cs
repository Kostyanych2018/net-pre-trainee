using Task4.Application.DTOs;
using Task4.Domain.Entities;

namespace Task4.Application.Services;

public interface IAuthorService
{
    Task<IEnumerable<AuthorDto>> GetAuthorsAsync();
    Task<AuthorDto?> GetAuthorByIdAsync(int id);
    Task<IEnumerable<AuthorDto>> GetAuthorsWithBooksAsync();
    Task<IEnumerable<AuthorWithBookCountDto>> GetAuthorsWithBooksCountAsync();
    Task<IEnumerable<AuthorDto>> SearchAuthorByNameAsync(string name, bool includeBooks );
    Task<AuthorDto> CreateAuthorAsync(CreateAuthorDto dto);
    Task UpdateAuthorAsync(int id, UpdateAuthorDto dto);
    Task DeleteAuthorAsync(int id);
    Task<bool> AuthorExistsAsync(int id);
}