using Microsoft.EntityFrameworkCore;
using Task4.Application.DTOs;
using Task4.Application.Mappers;
using Task4.Application.Services;
using Task4.Infrastructure.Data;

namespace Task4.Infrastructure.Services;

public class BookService : IBookService
{
    private readonly LibraryContext _context;

    public BookService(LibraryContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BookDto>> GetBooksAsync()
    {
        var books = await _context.Books.ToListAsync();
        return books.Select(BookMapper.ToDto);
    }

    public async Task<BookDto?> GetBookByIdAsync(int id)
    {
        var book = await _context.Books.FindAsync(id);
        return book == null ? null : BookMapper.ToDto(book);
    }

    public async Task<IEnumerable<BookDto>> GetBooksPublishedAfterYearAsync(int year)
    {
        var books = await _context.Books
            .Where(b => b.PublishedYear > year)
            .ToListAsync();
        return books.Select(BookMapper.ToDto);
    }

    public async Task<BookDto> CreateBookAsync(CreateBookDto dto)
    {
        var book = BookMapper.ToEntity(dto);

        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        return BookMapper.ToDto(book);
    }

    public async Task UpdateBookAsync(int id, UpdateBookDto dto)
    {
        var book = await _context.Books.FindAsync(id);
        if (book != null) {
            book.Title = dto.Title;
            book.PublishedYear = dto.PublishedYear;
            book.AuthorId = dto.AuthorId;

            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteBookAsync(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book != null) {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> BookExistsAsync(int id)
    {
        return await _context.Books.AnyAsync(b => b.Id == id);
    }
}