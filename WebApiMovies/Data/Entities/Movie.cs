using System.ComponentModel.DataAnnotations;
using WebApiMovies.Data.Entities.Intermediate_Entities;

namespace WebApiMovies.Data.Entities
{
    public class Movie : BaseEntity
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
        public string ImageUrl { get; set; } = null!;
        [Required]
        public string ImageId { get; set; } = null!;
        public ICollection<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();
        public ICollection<MovieActor> MovieActors { get; set; } = new List<MovieActor>();
        public ICollection<MovieReview> MovieReviews { get; set;} = new List<MovieReview>();

    }
}
