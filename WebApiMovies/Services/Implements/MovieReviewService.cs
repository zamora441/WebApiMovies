using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApiMovies.Data.Entities;
using WebApiMovies.Data_Access;
using WebApiMovies.Data_Access.Implements;
using WebApiMovies.DTOs;
using WebApiMovies.DTOs.MovieReviewsDTOs;
using WebApiMovies.Exceptions;
using WebApiMovies.HttpParameters;
using WebApiMovies.Utilities;

namespace WebApiMovies.Services.Implements
{
    public class MovieReviewService : IMovieReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public MovieReviewService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._userManager = userManager;
        }

        public async Task<MovieReviewDto> CreateMovieReviewAsync(int movieId, string userId, MovieReviewCreateDto movieReviewCreateDto)
        {
            var movieExists = await _unitOfWork.MovieRepository.GetMovieByIdAsync(movieId);
            if (movieExists == null)
            {
                throw new BadRequestException($"There is not movie with ID {movieId} in the system.");
            }

            var userExists = await _userManager.FindByIdAsync(userId);
            if(userExists is null)
            {
                throw new BadRequestException($"Thre is not user with Id {userId}.");
            }

            var movieReview = _mapper.Map<MovieReview>(movieReviewCreateDto);
            movieReview.MovieId = movieId;
            movieReview.UserId = userId;

            _unitOfWork.MovieReviewRepository.Create(movieReview);
            await _unitOfWork.SaveChangesAsync();

            var movieReviewDto = _mapper.Map<MovieReviewDto>(movieReview);
            return movieReviewDto;
        }

        public async Task DeleteMovieReviewAsync(int id, string currentUser)
        {
            var movieReview = await _unitOfWork.MovieReviewRepository.GetByCondition(mr => mr.Id == id).FirstOrDefaultAsync();
            if(movieReview is null)
            {
                throw new EntityNotFoundException(typeof(MovieReview), id);
            }
            
            if(currentUser != movieReview.UserId)
            {
                throw new UnauthorizedException("You do not have permission to delete this review.");
            }

            _unitOfWork.MovieReviewRepository.Delete(movieReview);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<PagedListDto<MovieReviewDto>> GetMovieReviewsAsync(int movieId, MovieReviewQueryParameters movieReviewQueryParameters)
        {
            var movieExists = await _unitOfWork.MovieRepository.GetMovieByIdAsync(movieId);
            if (movieExists == null)
            {
                throw new BadRequestException($"There is not movie with ID {movieId} to add a review.");
            }

            var movieReviews = await _unitOfWork.MovieReviewRepository.GetMovieReviewsAsync(movieId, movieReviewQueryParameters);
            var movieReviewsDtos = _mapper.Map<PagedListDto<MovieReviewDto>>(movieReviews);
            return movieReviewsDtos;
        }

        public async Task UpdateMovieReviewAsync(int id, string currentUser, MovieReviewCreateDto movieReviewCreateDto)
        {
            var movieReview = await _unitOfWork.MovieReviewRepository.GetByCondition(mr => mr.Id == id).FirstOrDefaultAsync();
            if (movieReview is null)
            {
                throw new EntityNotFoundException(typeof(MovieReview), id);

            }

            if (currentUser != movieReview.UserId)
            {
                throw new UnauthorizedException("You do not have permission to modify this review.");
            }

            _mapper.Map(movieReviewCreateDto, movieReview);
            _unitOfWork.MovieReviewRepository.Update(movieReview);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
