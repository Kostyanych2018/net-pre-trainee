using System.ComponentModel.DataAnnotations;
using Task4.Domain.ValidationAttributes;

namespace Task4.Domain.Entities;

public class Book
{
    public int Id { get; set; }

    [Required]
    [StringLength(150, MinimumLength = 5)]
    public string Title { get; set; }
    [NotInFutureYear]
    public int PublishedYear { get; set; }
    public int? AuthorId { get; set; }
}