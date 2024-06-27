using WebApiMovies.Data.Entities;
using WebApiMovies.Parameters;
using WebApiMovies.Utilities;

namespace WebApiMovies.Data_Access
{
    public interface IMovieRepository : IGenericRepository<Movie>
    {
        Task<PagedList<Movie>> GetMoviesAsync(MovieQueryParameters movieQueryParameters);
        Task<Movie?> GetMovieByIdAsync(int id);
        Task<Movie?> GetMovieDetailsByIdAsync(int id);
    }
}
