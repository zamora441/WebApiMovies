using AutoMapper;
using WebApiMovies.Data.Entities.Intermediate_Entities;
using WebApiMovies.DTOs.MovieActorDTOs;

namespace WebApiMovies.Profiles
{
    public class MovieActorProfile : Profile
    {
        public MovieActorProfile() { 
            CreateMap<MovieActor, MovieActorDetailsDto>();
            CreateMap<MovieActor, ActorIdAndNameDto>();
            CreateMap<MovieActorCreateDto, MovieActor>();
        }
    }
}
