using WebApiMovies.Data.Entities;

namespace WebApiMovies.Data_Access
{
    public interface IGenreRepository : IGenericRepository<Genre>
    {
        Task<ICollection<Genre>> GetGenresAsync();
   
    }
}
