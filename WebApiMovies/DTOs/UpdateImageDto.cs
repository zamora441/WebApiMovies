using WebApiMovies.Enums;
using WebApiMovies.Validations;

namespace WebApiMovies.DTOs
{
    public class UpdateImageDto
    {
        [ValidateFileType(fileType: FileTypes.Imagen)]
        public IFormFile File { get; set; } = null!;
    }
}
