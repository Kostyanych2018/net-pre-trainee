using Microsoft.EntityFrameworkCore;
using Task4.Application.DTOs;
using Task4.Application.Mappers;
using Task4.Application.Services;
using Task4.Domain.Entities;
using Task4.Infrastructure.Data;

namespace Task4.Infrastructure.Services;

public class AuthorService : IAuthorService
{
    private readonly LibraryContext _context;

    public AuthorService(LibraryContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AuthorDto>> GetAuthorsAsync()
    {
        var authors = await _context.Authors.ToListAsync();
        return authors.Select(AuthorMapper.ToDto);
    }

    public async Task<AuthorDto?> GetAuthorByIdAsync(int id)
    {
        var author = await _context.Authors
            .Include(a => a.Books)
            .FirstOrDefaultAsync(a => a.Id == id);

        return author == null ? null : AuthorMapper.ToDto(author);
    }

    public async Task<IEnumerable<AuthorDto>> GetAuthorsWithBooksAsync()
    {
        var authors = await _context.Authors
            .Include(a => a.Books)
            .ToListAsync();

        return authors.Select(AuthorMapper.ToDto);
    }

    public async Task<IEnumerable<AuthorWithBookCountDto>> GetAuthorsWithBooksCountAsync()
    {
        return await _context.Authors
            .Select(author => new AuthorWithBookCountDto()
            {
                Id = author.Id,
                Name = author.Name,
                DateOfBirth = author.DateOfBirth,
                BookCount = author.Books.Count()
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<AuthorDto>> SearchAuthorByNameAsync(string name, bool includeBooks = false)
    {
        var query = _context.Authors.AsQueryable();
        if (includeBooks) {
            query = query.Include(a => a.Books);
        }

        var authors = await query
            .Where(a => a.Name.ToLower().Contains(name.ToLower()))
            .ToListAsync();

        return authors.Select(AuthorMapper.ToDto);
    }

    public async Task<AuthorDto> CreateAuthorAsync(CreateAuthorDto dto)
    {
        var author = AuthorMapper.ToEntity(dto);

        _context.Authors.Add(author);
        await _context.SaveChangesAsync();

        return AuthorMapper.ToDto(author);
    }

    public async Task UpdateAuthorAsync(int id, UpdateAuthorDto dto)
    {
        var author = await _context.Authors.FindAsync(id);
        if (author != null) {
            author.Name = dto.Name;
            author.DateOfBirth = dto.DateOfBirth;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAuthorAsync(int id)
    {
        var author = await _context.Authors.FindAsync(id);
        if (author == null) return;

        await _context.Books
            .Where(b => b.AuthorId == author.Id)
            .ExecuteUpdateAsync(updates => updates.SetProperty(b => b.AuthorId, (int?)null));

        _context.Authors.Remove(author);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> AuthorExistsAsync(int id)
    {
        return await _context.Authors.AnyAsync(a => a.Id == id);
    }
}