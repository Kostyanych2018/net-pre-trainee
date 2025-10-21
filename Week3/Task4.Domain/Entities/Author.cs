using System.ComponentModel.DataAnnotations;
using Task4.Domain.ValidationAttributes;

namespace Task4.Domain.Entities;

public class Author
{
    public int Id { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; }

    [PastDate] 
    public DateTime DateOfBirth { get; set; }
}