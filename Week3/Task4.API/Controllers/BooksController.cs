using Microsoft.AspNetCore.Mvc;
using Task4.Domain.Entities;
using Task4.Infrastructure.Data;

namespace Task4.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Book>> GetBooks()
    {
        return Ok(InMemoryStore.Books);
    }

    [HttpGet("{id:int}")]
    public ActionResult<Book> GetBook(int id)
    {
        var book = InMemoryStore.Books.FirstOrDefault(b => b.Id == id);
        if (book == null) {
            return NotFound();
        }

        return Ok(book);
    }

    [HttpPost]
    public ActionResult<Book> PostBook(Book book)
    {
        if (!InMemoryStore.Authors.Any(a => a.Id == book.AuthorId)) {
            return BadRequest("Author with the specified ID does not exist.");
        }

        book.Id = InMemoryStore.Books.Any() ? InMemoryStore.Books.Max(b => b.Id) + 1 : 1;
        InMemoryStore.Books.Add(book);
        return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
    }

    [HttpPut("{id:int}")]
    public IActionResult PutBook(int id, Book bookToUpdate)
    {
        if (id != bookToUpdate.Id) {
            return BadRequest("The ID from the URL does not match the ID in the request body");
        }

        var book = InMemoryStore.Books.FirstOrDefault(b => b.Id == id);
        if (book == null) {
            return NotFound();
        }

        if (!InMemoryStore.Authors.Any(a => a.Id == bookToUpdate.AuthorId)) {
            return BadRequest("Author with the specified ID does not exist.");
        }

        book.Title = bookToUpdate.Title;
        book.AuthorId = bookToUpdate.AuthorId;
        book.PublishedYear = bookToUpdate.PublishedYear;

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteBook(int id)
    {
        var book = InMemoryStore.Books.FirstOrDefault(b => b.Id == id);
        if (book == null) {
            return NotFound();
        }

        InMemoryStore.Books.Remove(book);
        return NoContent();
    }
}