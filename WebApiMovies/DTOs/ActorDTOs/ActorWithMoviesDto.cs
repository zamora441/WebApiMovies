using WebApiMovies.DTOs.MovieActorDTOs;

namespace WebApiMovies.DTOs.ActorDTOs
{
    public class ActorWithMoviesDto : ActorDto
    {
        public ICollection<MovieActorDetailsDto> Movies { get; set; } = new List<MovieActorDetailsDto>();
    }
}
