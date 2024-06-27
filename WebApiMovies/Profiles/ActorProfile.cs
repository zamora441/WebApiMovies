using AutoMapper;
using WebApiMovies.Data.Entities;
using WebApiMovies.DTOs.ActorDTOs;
using WebApiMovies.Extensions;

namespace WebApiMovies.Profiles
{
    public class ActorProfile : Profile
    {
        public ActorProfile() {
            CreateMap<Actor, ActorWithMoviesDto>()
                .ForMember(dest => dest.Movies,
                            opt => opt.MapFrom(src => src.MovieActors));
            CreateMap<Actor, ActorDto>();
            CreateMap<ActorCreateDto, Actor>()
                .ForMember(dest => dest.Name,
                            opt => opt.MapFrom(src => src.Name.ToTitleCase()))
                .ForMember(dest => dest.LastName,
                            opt => opt.MapFrom(src => src.LastName.ToTitleCase()));
            CreateMap<ActorUpdateDto, Actor>()
                .ForMember(dest => dest.Name,
                            opt => opt.MapFrom(src => src.Name.ToTitleCase()))
                .ForMember(dest => dest.LastName,
                            opt => opt.MapFrom(src => src.LastName.ToTitleCase()));

        }
    }
}
