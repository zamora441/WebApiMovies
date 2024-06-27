using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WebApiMovies.Data;
using WebApiMovies.Data.Entities;
using WebApiMovies.HttpParameters;
using WebApiMovies.Utilities;

namespace WebApiMovies.Data_Access.Implements
{
    public class MovieReviewRepository : GenericRepository<MovieReview>, IMovieReviewRepository
    {
        public MovieReviewRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public async Task<PagedList<MovieReview>> GetMovieReviewsAsync(int movieId, MovieReviewQueryParameters movieReviewQueryParameters)
        {
            IQueryable<MovieReview> queryable = GetByCondition(mr => mr.MovieId == movieId)
                                        .Include(mr => mr.User)
                                        .Select(mr => new MovieReview
                                        {
                                            Id = mr.Id,
                                            Review = mr.Review,
                                            MovieId = mr.MovieId,
                                            UserId = mr.User.Id,
                                            User = new User { UserName = mr.User.UserName }
                                        });
                                        
            return await PagedList<MovieReview>.ToPagedListAsync(queryable, movieReviewQueryParameters.PageNumber, movieReviewQueryParameters.PageSize);
        }
    }
}
