using System.ComponentModel.DataAnnotations;

namespace WebApiMovies.Data.Entities
{
    public class MovieReview : BaseEntity
    {
        [Required]
        public string Review { get; set; } = null!;
        [Required]
        public int MovieId { get; set; }
        public Movie Movie { get; set; } = null!;
        [Required]
        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
