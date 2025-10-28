using System.ComponentModel.DataAnnotations;
using Task4.Domain.ValidationAttributes;

namespace Task4.Application.DTOs;

public class BookDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int PublishedYear { get; set; }
    public int? AuthorId { get; set; }
}

public class CreateBookDto
{
    [Required]
    [StringLength(150, MinimumLength = 5)]
    public string Title { get; set; }
    
    [Required]
    [NotInFutureYear]
    public int PublishedYear { get; set; }
    
    [Required]
    public int? AuthorId { get; set; }
}

public class UpdateBookDto
{
    [Required]
    [StringLength(150, MinimumLength = 5)]
    public string Title { get; set; }
    
    [NotInFutureYear]
    public int PublishedYear { get; set; }
    
    public int? AuthorId { get; set; }
}