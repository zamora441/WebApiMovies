using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;
using WebApiMovies.Data;
using WebApiMovies.Data.Entities;
using WebApiMovies.Extensions;
using WebApiMovies.Parameters;
using WebApiMovies.Utilities;

namespace WebApiMovies.Data_Access.Implements
{
    public sealed class ActorRepository : GenericRepository<Actor>, IActorRepository
    {
        public ActorRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { }


        public async Task<Actor?> GetActorByIdAsync(int id)
        {
            return await GetByCondition(a => a.Id == id)
                .Include(a => a.Country)
                .Include(a=> a.MovieActors)
                    .ThenInclude(ma => ma.Movie)
                .FirstOrDefaultAsync();
        }

        public async Task<PagedList<Actor>> GetActorsAsync(ActorQueryParameters actorQueryParameters)
        {
            IQueryable<Actor> queryable = GetAll().Include(a => a.MovieActors);

            queryable = SearchByName(queryable, actorQueryParameters);

            return await PagedList<Actor>.ToPagedListAsync(queryable.OrderByDinamyc(actorQueryParameters.OrderBy!), actorQueryParameters.PageNumber, actorQueryParameters.PageSize);
        }

        private IQueryable<Actor> SearchByName (IQueryable<Actor> queryable, ActorQueryParameters actorQueryParameters)
        {
            if (!string.IsNullOrEmpty(actorQueryParameters.SearchString))
            {
                var searchString = actorQueryParameters.SearchString.Trim().ToLower();
                queryable = queryable.Where(actor => actor.Name.ToLower().Contains(searchString) || actor.LastName.ToLower().Contains(searchString));
            }

            return queryable;
        }
    }
}
 