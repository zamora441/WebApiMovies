using AutoMapper;
using WebApiMovies.Data.Entities;
using WebApiMovies.DTOs.CountryDTOs;

namespace WebApiMovies.Profiles
{
    public class CountryProfile : Profile
    {
        public CountryProfile() { 
            CreateMap<Country, CountryDto>();
            CreateMap<Country, CountryNameDto>();
        }
    }
}
