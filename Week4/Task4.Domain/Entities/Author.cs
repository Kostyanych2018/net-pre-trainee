using System.ComponentModel.DataAnnotations;
using Task4.Domain.ValidationAttributes;

namespace Task4.Domain.Entities;

public class Author
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
    public ICollection<Book> Books { get; set; } = [];
}