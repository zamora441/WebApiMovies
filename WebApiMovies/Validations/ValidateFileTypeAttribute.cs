using System.ComponentModel.DataAnnotations;
using WebApiMovies.Enums;

namespace WebApiMovies.Validations
{
    public class ValidateFileTypeAttribute : ValidationAttribute
    {
        private readonly List<string> validTypes = new List<string>();

        public ValidateFileTypeAttribute(FileTypes fileType)
        {
            if(fileType == FileTypes.Imagen)
            {
                validTypes.AddRange(new[] { "image/jpeg", "image/png", "image/gif", "image/bmp" });
            }
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value is null)
            {
                return ValidationResult.Success;
            }

            var file = value as IFormFile;
            if (!validTypes.Contains(file!.ContentType))
            {
                return new ValidationResult($"The file type is not allowed. Only file types are allowed: {string.Join(", ", validTypes)}.");
            }

            return ValidationResult.Success;

        }
    }
}
