using AutoMapper;
using WebApiMovies.Data.Entities;
using WebApiMovies.DTOs;

namespace WebApiMovies.Profiles
{
    public class GenreProfile : Profile
    {
        public GenreProfile() {
            CreateMap<Genre, GenreDto>();
        }
    }
}
