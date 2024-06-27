using System.ComponentModel.DataAnnotations;

namespace WebApiMovies.DTOs.MovieActorDTOs
{
    public class MovieActorCreateDto
    {
        [Required]
        public int ActorId { get; set; }
        [Required]
        [StringLength(50)]
        public string Character { get; set; } = null!;
    }
}
