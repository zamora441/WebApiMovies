using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiMovies.DTOs.ActorDTOs;
using WebApiMovies.DTOs.CountryDTOs;
using WebApiMovies.Services;

namespace WebApiMovies.Controllers
{
    [ApiController]
    [Route("api/countries")]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountriesController(ICountryService countryService)
        {
            this._countryService = countryService;
        }

        [HttpGet(Name = "GetCountries")]
        public async Task<ActionResult<List<CountryDto>>> Get()
        {
            var countriesDtos = await _countryService.GetCountriesAsync();
            return Ok(countriesDtos);
        }

    }
}
