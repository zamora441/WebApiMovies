using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using WebApiMovies.Data_Access;
using WebApiMovies.DTOs.CountryDTOs;

namespace WebApiMovies.Services.Implements
{
    public sealed class CountryService : ICountryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CountryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<CountryDto>> GetCountriesAsync()
        {
            var countries= await _unitOfWork.CountryRepository.GetCountriesAsync();
            var countriesDtos = _mapper.Map<List<CountryDto>>(countries);
            return countriesDtos;
        }
    }
}
