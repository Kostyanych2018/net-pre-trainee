using Microsoft.AspNetCore.Mvc;
using Task4.Application.DTOs;
using Task4.Application.Services;
using Task4.Domain.Entities;

namespace Task4.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorService _authorService;

    public AuthorsController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAuthors()
    {
        return Ok(await _authorService.GetAuthorsAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AuthorDto>> GetAuthor(int id)
    {
        var author = await _authorService.GetAuthorByIdAsync(id);
        return author == null ? NotFound() : Ok(author);
    }

    [HttpGet("with-books")]
    public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAuthorsWithBooks()
    {
        return Ok(await _authorService.GetAuthorsWithBooksAsync());
    }

    [HttpGet("with-book-count")]
    public async Task<ActionResult<IEnumerable<AuthorWithBookCountDto>>> GetAuthorsWithBookCount()
    {
        return Ok(await _authorService.GetAuthorsWithBooksCountAsync());
    }

    [HttpPost]
    public async Task<ActionResult<AuthorDto>> PostAuthor(CreateAuthorDto dto)
    {
        var createdAuthor = await _authorService.CreateAuthorAsync(dto);
        return CreatedAtAction(nameof(GetAuthor), new { id = createdAuthor.Id }, createdAuthor);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutAuthor(int id, UpdateAuthorDto dto)
    {
        if (!await _authorService.AuthorExistsAsync(id)) {
            return NotFound();
        }

        await _authorService.UpdateAuthorAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        if (!await _authorService.AuthorExistsAsync(id)) {
            return NotFound();
        }
        
        await _authorService.DeleteAuthorAsync(id);
        return NoContent();
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<Author>>> SearchAuthorByNameAsync(
        [FromQuery] string? name,
        [FromQuery] bool includeBooks = false)
    {
        if (string.IsNullOrWhiteSpace(name)) {
            return BadRequest("Name query parameter cannot be empty.");
        }

        var authors = await _authorService.SearchAuthorByNameAsync(name, includeBooks);
        return Ok(authors);
    }
}