using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace WebApiMovies.Validations
{
    public class ValidateListAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
            {
                return ValidationResult.Success;
            }

            var list = value as IList;
            if(list!.Count == 0) {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
