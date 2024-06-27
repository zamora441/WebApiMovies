using System.ComponentModel.DataAnnotations;
using WebApiMovies.DTOs.MovieActorDTOs;
using WebApiMovies.Validations;

namespace WebApiMovies.DTOs.MovieDTOs
{
    public class MovieUpdateDto
    {
        [Required]
        [StringLength(150)]
        public string Title { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;
        [Required]
        public int DurationMinutes { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        [Required]
        [ValidateList(ErrorMessage = "You must add at least one genre.")]
        public List<int> GenresIds { get; set; } = new List<int>();
        [Required]
        [ValidateList(ErrorMessage = "You must add at least one actor.")]
        public ICollection<MovieActorCreateDto> Actors { get; set; } = new List<MovieActorCreateDto>();
    }
}
