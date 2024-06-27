using AutoMapper;
using WebApiMovies.Data.Entities;
using WebApiMovies.DTOs;
using WebApiMovies.DTOs.ActorDTOs;
using WebApiMovies.DTOs.MovieDTOs;
using WebApiMovies.DTOs.MovieReviewsDTOs;
using WebApiMovies.Utilities;

namespace WebApiMovies.Profiles
{
    public class PagedListProfile : Profile
    {
        public PagedListProfile() {
            CreateMap<PagedList<Movie>, PagedListDto<MovieDto>>().
                ForMember(dest => dest.Items, opt => opt.MapFrom(src => src));
            CreateMap<PagedList<Actor>, PagedListDto<ActorDto>>().
                ForMember(dest => dest.Items, opt => opt.MapFrom(src => src));
            CreateMap<PagedList<MovieReview>, PagedListDto<MovieReviewDto>>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src));
        }
    }
}
