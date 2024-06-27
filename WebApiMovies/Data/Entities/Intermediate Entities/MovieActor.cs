using System.ComponentModel.DataAnnotations;

namespace WebApiMovies.Data.Entities.Intermediate_Entities
{
    public class MovieActor
    {
        [Required]
        public int MovieId { get; set; }
        [Required]
        public int ActorId { get; set; }
        [Required]
        [StringLength(50)]
        public string Character { get; set; } = null!;
        public Movie Movie { get; set; } = null!;
        public Actor Actor { get; set; } = null!;

    }
}
