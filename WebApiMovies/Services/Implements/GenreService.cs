using AutoMapper;
using WebApiMovies.Data_Access;
using WebApiMovies.DTOs;

namespace WebApiMovies.Services.Implements
{
    public sealed class GenreService : IGenreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GenreService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<List<GenreDto>> GetGenresAsync()
        {
            var genres = await _unitOfWork.GenreRepository.GetGenresAsync();
            var genresDtos = _mapper.Map<List<GenreDto>>(genres);
            return genresDtos;
        }
    }
}
