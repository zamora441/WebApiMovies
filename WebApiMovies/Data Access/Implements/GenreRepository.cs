using Microsoft.EntityFrameworkCore;
using WebApiMovies.Data;
using WebApiMovies.Data.Entities;

namespace WebApiMovies.Data_Access.Implements
{
    public sealed class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        public GenreRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { }


        public async Task<ICollection<Genre>> GetGenresAsync()
        {
            return await GetAll().ToListAsync();
        }

       
    }
}
