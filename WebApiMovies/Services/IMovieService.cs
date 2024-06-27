using WebApiMovies.DTOs;
using WebApiMovies.DTOs.MovieDTOs;
using WebApiMovies.Parameters;

namespace WebApiMovies.Services
{
    public interface IMovieService
    {
        Task<PagedListDto<MovieDto>> GetMoviesAsync(MovieQueryParameters movieQueryParameters);
        Task<MovieWithDetailsDto> GetMovieByIdAsync(int id);
        Task<MovieDto> CreateMovieAsync(MovieCreateDto movieCreateDto);
        Task UpdateMovieAsync(int id, MovieUpdateDto movieUpdateDto);
        Task<string> UpdateMovieImageAsync(int id, UpdateImageDto updateImageDto);
        Task DeleteMovieAsync(int id);
    }
}
