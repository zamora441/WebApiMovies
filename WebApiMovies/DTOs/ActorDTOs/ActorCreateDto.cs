
using WebApiMovies.Enums;
using WebApiMovies.Validations;

namespace WebApiMovies.DTOs.ActorDTOs
{
    public class ActorCreateDto : ActorUpdateDto
    {
        [ValidateFileType(fileType: FileTypes.Imagen)]
        public IFormFile? ActorImage { get; set; }

    }
}
