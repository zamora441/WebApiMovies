using AutoMapper;
using WebApiMovies.Data.Entities;
using WebApiMovies.Data.Entities.Intermediate_Entities;
using WebApiMovies.DTOs;
using WebApiMovies.DTOs.MovieDTOs;
using WebApiMovies.Extensions;

namespace WebApiMovies.Profiles
{
    public class MovieProfile : Profile
    { 
        public MovieProfile() { 
            CreateMap<Movie, MovieDto>();
            CreateMap<Movie, MovieWithDetailsDto>()
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.MovieGenres.Select(mg => new GenreDto
                {
                    Id = mg.GenreId,
                    Name = mg.Genre.Name
                })))
                .ForMember(dest => dest.Actors,
                            opt => opt.MapFrom(src => src.MovieActors));
            CreateMap<MovieCreateDto, Movie>()
                .ForMember(dest => dest.MovieGenres, opt => opt.MapFrom(src => src.GenresIds.Select(genreId => new MovieGenre
                            {
                                GenreId = genreId
                            })))
                .ForMember(dest => dest.MovieActors,
                            opt => opt.MapFrom(src => src.Actors))
                .ForMember(dest => dest.Title,
                            opt => opt.MapFrom(src => src.Title.ToTitleCase()));
            CreateMap<MovieUpdateDto, Movie>()
                .ForMember(dest => dest.MovieGenres, opt => opt.MapFrom(src => src.GenresIds.Select(genreId => new MovieGenre
                {
                    GenreId = genreId
                })))
                .ForMember(dest => dest.MovieActors,
                            opt => opt.MapFrom(src => src.Actors))
                .ForMember(dest => dest.Title,
                            opt => opt.MapFrom(src => src.Title.ToTitleCase()));
        }
    }
}
