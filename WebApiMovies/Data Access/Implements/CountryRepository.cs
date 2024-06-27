using Microsoft.EntityFrameworkCore;
using WebApiMovies.Data;
using WebApiMovies.Data.Entities;

namespace WebApiMovies.Data_Access.Implements
{
    public sealed class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        public CountryRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { }


        public async Task<ICollection<Country>> GetCountriesAsync()
        {
            return await GetAll().ToListAsync();
        }

        public async Task<Country?> GetCountryByIdAsync(int id)
        {
            return await GetByCondition(country => country.Id == id).FirstOrDefaultAsync();
            
        }
    }
}
