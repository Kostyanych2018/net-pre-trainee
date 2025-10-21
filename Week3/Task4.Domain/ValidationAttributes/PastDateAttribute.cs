using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace Task4.Domain.ValidationAttributes;

public class PastDateAttribute : ValidationAttribute
{
    public PastDateAttribute()
    {
        ErrorMessage = "The date must be in the past.";
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is DateTime date) {
            if (date < DateTime.Now) {
                return ValidationResult.Success;
            }
        }

        return new ValidationResult(ErrorMessage);
    }
}