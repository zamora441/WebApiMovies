
namespace WebApiMovies.DTOs.MovieDTOs
{
    public class MovieDto 
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int DurationMinutes { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string ImageUrl { get; set; } = null!;
    }
}
