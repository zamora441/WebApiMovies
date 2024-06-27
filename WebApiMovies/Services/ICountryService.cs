using WebApiMovies.Data.Entities;
using WebApiMovies.DTOs.CountryDTOs;

namespace WebApiMovies.Services
{
    public interface ICountryService
    {
        Task<List<CountryDto>> GetCountriesAsync();
    }
}
