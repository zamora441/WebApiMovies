using AutoMapper;
using Microsoft.AspNetCore.Identity;
using WebApiMovies.Data.Entities;
using WebApiMovies.DTOs.AuthDTOs;

namespace WebApiMovies.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile() {
            CreateMap<UserRegisterDto, User>();
        }
    }
}
