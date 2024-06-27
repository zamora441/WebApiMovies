using WebApiMovies.DTOs;

namespace WebApiMovies.Services
{
    public interface IGenreService
    {
        Task<List<GenreDto>> GetGenresAsync();
    }
}
