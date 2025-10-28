using System.ComponentModel.DataAnnotations;

namespace Task4.Domain.ValidationAttributes;

public class NotInFutureYearAttribute : ValidationAttribute
{
    public NotInFutureYearAttribute()
    {
        ErrorMessage = "The published year cannot be in the future.";
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is int year) {
            if (year <= DateTimeOffset.Now.Year) {
                return ValidationResult.Success;
            }
        }

        return new ValidationResult(ErrorMessage);
    }
}