using Microsoft.AspNetCore.Mvc;
using Task4.Application.DTOs;
using Task4.Application.Services;
using Task4.Domain.Entities;

namespace Task4.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IAuthorService _authorService;
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService, IAuthorService authorService)
    {
        _bookService = bookService;
        _authorService = authorService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks()
    {
        return Ok(await _bookService.GetBooksAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<BookDto>> GetBook(int id)
    {
        var book = await _bookService.GetBookByIdAsync(id);
        return book == null ? NotFound() : Ok(book);
    }

    [HttpGet("published-after/{year:int}")]
    public async Task<ActionResult<IEnumerable<BookDto>>> GetBooksPublishedAfterYear(int year)
    {
        return Ok(await _bookService.GetBooksPublishedAfterYearAsync(year));
    }

    [HttpPost]
    public async Task<ActionResult<BookDto>> PostBook(CreateBookDto dto)
    {
        if (dto.AuthorId.HasValue && !await _authorService.AuthorExistsAsync(dto.AuthorId.Value)) {
            return BadRequest("Author with the specified ID does not exist.");
        }

        var createdBook = await _bookService.CreateBookAsync(dto);
        return CreatedAtAction(nameof(GetBook), new { id = createdBook.Id }, createdBook);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutBook(int id, UpdateBookDto dto)
    {
        if (!await _bookService.BookExistsAsync(id)) {
            return NotFound();
        }

        if (dto.AuthorId.HasValue && !await _authorService.AuthorExistsAsync(dto.AuthorId.Value)) {
            return BadRequest("Book author with the specified ID does not exist.");
        }

        await _bookService.UpdateBookAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        if (!await _bookService.BookExistsAsync(id)) {
            return NotFound();
        }

        await _bookService.DeleteBookAsync(id);
        return NoContent();
    }
}