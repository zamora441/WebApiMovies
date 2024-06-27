using WebApiMovies.Data.Entities;

namespace WebApiMovies.Data_Access
{
    public interface ICountryRepository : IGenericRepository<Country>
    {
        Task<ICollection<Country>> GetCountriesAsync();
        Task<Country?> GetCountryByIdAsync(int id);
    }
}
