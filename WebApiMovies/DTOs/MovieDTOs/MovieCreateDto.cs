
using WebApiMovies.Enums;
using WebApiMovies.Validations;

namespace WebApiMovies.DTOs.MovieDTOs
{
    public class MovieCreateDto : MovieUpdateDto
    {
        [ValidateFileType(fileType: FileTypes.Imagen)]
        public IFormFile? MovieImage { get; set; }
       
    }
}
 