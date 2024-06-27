using WebApiMovies.Data.Entities;
using WebApiMovies.Parameters;
using WebApiMovies.Utilities;

namespace WebApiMovies.Data_Access
{
    public interface IActorRepository : IGenericRepository<Actor>
    {
        Task<PagedList<Actor>> GetActorsAsync(ActorQueryParameters actorQueryParameters);
        Task<Actor?> GetActorByIdAsync(int id);
    }
}
