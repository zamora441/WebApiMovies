using WebApiMovies.Data.Entities.Intermediate_Entities;
using WebApiMovies.DTOs.MovieActorDTOs;

namespace WebApiMovies.DTOs.MovieDTOs
{
    public class MovieWithDetailsDto : MovieDto
    {
        public ICollection<GenreDto> Genres { get; set; } = new List<GenreDto>();
        public ICollection<ActorIdAndNameDto> Actors { get; set; } = new List<ActorIdAndNameDto>();
    }
}
