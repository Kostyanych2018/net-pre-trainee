using System.ComponentModel.DataAnnotations;
using Task4.Domain.ValidationAttributes;


namespace Task4.Application.DTOs;

public class AuthorDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
    public ICollection<BookDto> Books { get; set; } = [];
}

public class AuthorWithBookCountDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
    public int BookCount { get; set; }
}

public class CreateAuthorDto
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; }

    [Required]
    [PastDate]
    public DateTimeOffset DateOfBirth { get; set; }
}

public class UpdateAuthorDto
{
    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; }

    [PastDate]
    public DateTimeOffset DateOfBirth { get; set; }
}