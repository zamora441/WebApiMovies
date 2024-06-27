using WebApiMovies.DTOs.MovieActorDTOs;

namespace WebApiMovies.DTOs.ActorDTOs
{
    public class ActorDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public int Age { get; set; }
        public string CountryName { get; set; } = null!;
        public string? ImageUrl { get; set; } 
    }
}
