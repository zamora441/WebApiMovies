namespace WebApiMovies.DTOs.MovieActorDTOs
{
    public class MovieActorDetailsDto
    {
        public int MovieId { get; set; }
        public string Character { get; set; } = null!;
        public string MovieTitle { get; set; } = null!;
        public string MovieImageUrl { get; set; } = null!;
        public DateTime MovieReleaseDate { get; set; }
    }
}
