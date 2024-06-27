using WebApiMovies.DTOs;
using WebApiMovies.DTOs.MovieReviewsDTOs;
using WebApiMovies.HttpParameters;
using WebApiMovies.Utilities;

namespace WebApiMovies.Services
{
    public interface IMovieReviewService
    {
        Task<PagedListDto<MovieReviewDto>> GetMovieReviewsAsync(int movieId, MovieReviewQueryParameters movieReviewQueryParameters);
        Task<MovieReviewDto> CreateMovieReviewAsync(int movieId, string userId, MovieReviewCreateDto movieReviewCreateDto);
        Task UpdateMovieReviewAsync(int id, string currentUser, MovieReviewCreateDto movieReviewCreateDto);
        Task DeleteMovieReviewAsync(int id, string currentUser);
    }
}
