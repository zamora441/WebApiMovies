using WebApiMovies.Data.Entities;
using WebApiMovies.HttpParameters;
using WebApiMovies.Utilities;

namespace WebApiMovies.Data_Access
{
    public interface IMovieReviewRepository : IGenericRepository<MovieReview>
    {
        Task<PagedList<MovieReview>> GetMovieReviewsAsync(int movieId, MovieReviewQueryParameters movieReviewQueryParameters);
    }
}
