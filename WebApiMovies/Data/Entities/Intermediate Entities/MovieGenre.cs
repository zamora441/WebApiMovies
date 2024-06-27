using System.ComponentModel.DataAnnotations;

namespace WebApiMovies.Data.Entities.Intermediate_Entities
{
    public class MovieGenre
    {
        [Required]
        public int MovieId { get; set; }
        [Required]
        public int GenreId { get; set;}
        public Movie Movie { get; set; } = null!;
        public Genre Genre { get; set; } = null!;
    }
}
