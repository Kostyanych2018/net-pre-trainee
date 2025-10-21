using Microsoft.AspNetCore.Mvc;
using Task4.Domain.Entities;
using Task4.Infrastructure.Data;

namespace Task4.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Author>> GetAuthors()
    {
        return Ok(InMemoryStore.Authors);
    }

    [HttpGet("{id:int}")]
    public ActionResult<Author> GetAuthor(int id)
    {
        var author = InMemoryStore.Authors.FirstOrDefault(a => a.Id == id);
        if (author == null) {
            return NotFound();
        }

        return Ok(author);
    }

    [HttpPost]
    public ActionResult<Author> PostAuthor(Author author)
    {
        author.Id = InMemoryStore.Authors.Any() ? InMemoryStore.Authors.Max(a => a.Id) + 1 : 1;
        InMemoryStore.Authors.Add(author);
        return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
    }

    [HttpPut("{id:int}")]
    public IActionResult PutAuthor(int id, Author authorToUpdate)
    {
        if (id != authorToUpdate.Id) {
            return BadRequest("The ID from the URL does not match the ID in the request body.");
        }

        var author = InMemoryStore.Authors.FirstOrDefault(a => a.Id == id);
        if (author == null) {
            return NotFound();
        }

        author.Name = authorToUpdate.Name;
        author.DateOfBirth = authorToUpdate.DateOfBirth;
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteAuthor(int id)
    {
        var author = InMemoryStore.Authors.FirstOrDefault(a => a.Id == id);
        if (author == null) {
            return NotFound();
        }

        var books = InMemoryStore.Books.Where(b => b.AuthorId == id);
        foreach (var book in books) {
            book.AuthorId = null;
        }

        InMemoryStore.Authors.Remove(author);
        return NoContent();
    }
}