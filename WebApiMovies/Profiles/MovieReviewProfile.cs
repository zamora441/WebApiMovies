using AutoMapper;
using WebApiMovies.Data.Entities;
using WebApiMovies.DTOs.MovieReviewsDTOs;

namespace WebApiMovies.Profiles
{
    public class MovieReviewProfile : Profile
    {
        public MovieReviewProfile()
        {
            CreateMap<MovieReviewCreateDto, MovieReview>();
            CreateMap<MovieReview, MovieReviewDto>();
        }
    }
}
