using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApiMovies.Data;
using WebApiMovies.Data.Entities;
using WebApiMovies.Extensions;
using WebApiMovies.Parameters;
using WebApiMovies.Utilities;

namespace WebApiMovies.Data_Access.Implements
{
    public sealed class MovieRepository : GenericRepository<Movie>, IMovieRepository
    {
        public MovieRepository(ApplicationDbContext applicationDbContext): base(applicationDbContext) { 
        }


        public async Task<Movie?> GetMovieDetailsByIdAsync(int id)
        {
            return await GetByCondition(movie => movie.Id == id)
                .Include(m=> m.MovieGenres)
                    .ThenInclude(mg => mg.Genre)
                .Include(m=> m.MovieActors)
                    .ThenInclude(ma => ma.Actor)
                .FirstOrDefaultAsync();
        }

        public async Task<Movie?> GetMovieByIdAsync(int id)
        {
            return await GetByCondition(movie => movie.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<PagedList<Movie>> GetMoviesAsync(MovieQueryParameters movieQueryParameters)
        {
            IQueryable<Movie> queryable = GetAll();

            queryable = SearchByTitle(queryable, movieQueryParameters);

            queryable = ApplySearchFilters(queryable, movieQueryParameters);

            return await PagedList<Movie>.ToPagedListAsync(queryable.OrderByDinamyc(movieQueryParameters.OrderBy!), movieQueryParameters.PageNumber, movieQueryParameters.PageSize);
        }
      

        private IQueryable<Movie> SearchByTitle(IQueryable<Movie> queryable, MovieQueryParameters movieQueryParameters)
        {
            if (!string.IsNullOrEmpty(movieQueryParameters.SearchString))
            {
                var searchString = movieQueryParameters.SearchString.Trim().ToLower();
                queryable = queryable.Where(movie => movie.Title.ToLower().Contains(searchString));
            }
            
            return queryable;
        }

        private IQueryable<Movie> ApplySearchFilters(IQueryable<Movie> queryable, MovieQueryParameters movieQueryParameters)
        {
            if (movieQueryParameters.Genre.HasValue)
            {
                queryable = queryable.Where(m => m.MovieGenres.Any(mg => mg.GenreId == movieQueryParameters.Genre));
            }

            return queryable;
        }
    }
}
