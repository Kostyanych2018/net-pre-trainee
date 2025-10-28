using Task4.Application.DTOs;
using Task4.Domain.Entities;

namespace Task4.Application.Services;

public interface IBookService
{
    Task<IEnumerable<BookDto>> GetBooksAsync();
    Task<BookDto?> GetBookByIdAsync(int id); 
    Task<IEnumerable<BookDto>> GetBooksPublishedAfterYearAsync(int year);
    Task<BookDto> CreateBookAsync(CreateBookDto dto);
    Task UpdateBookAsync(int id,UpdateBookDto dto);
    Task DeleteBookAsync(int id);
    Task<bool> BookExistsAsync(int id);
}