using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApiMovies.Data.Entities;
using WebApiMovies.Data_Access;
using WebApiMovies.Data_Access.Implements;
using WebApiMovies.DTOs;
using WebApiMovies.DTOs.ActorDTOs;
using WebApiMovies.DTOs.MovieActorDTOs;
using WebApiMovies.DTOs.MovieDTOs;
using WebApiMovies.Exceptions;
using WebApiMovies.Parameters;

namespace WebApiMovies.Services.Implements
{
    public sealed class MovieService : IMovieService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUploadFileService _uploadFileService;

        public MovieService(IUnitOfWork unitOfWork, IMapper mapper, IUploadFileService uploadFileService)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._uploadFileService = uploadFileService;
        }

        public async Task<MovieDto> CreateMovieAsync(MovieCreateDto movieCreateDto)
        {
            await VerifyActorsExist(movieCreateDto);

            await VerifyGenresExist(movieCreateDto.GenresIds);

            var movie = _mapper.Map<Movie>(movieCreateDto);

            if (movieCreateDto.MovieImage is not null)
            {
                var uploadResult = await _uploadFileService.UploadFileAsync(movieCreateDto.MovieImage, "Movies");
                movie.ImageUrl = uploadResult.Url.ToString();
                movie.ImageId = uploadResult.PublicId;
            }

            _unitOfWork.MovieRepository.Create(movie);
            await _unitOfWork.SaveChangesAsync();

            var movieDto = _mapper.Map<MovieDto>(movie);
            return movieDto;
        }

        public async Task DeleteMovieAsync(int id)
        {
            var movie = await _unitOfWork.MovieRepository.GetMovieByIdAsync(id);
            if(movie is null)
            {
                throw new EntityNotFoundException(typeof(Movie), id);
            }

            _unitOfWork.MovieRepository.Delete(movie);
            await _unitOfWork.SaveChangesAsync();
            
        }

        public async Task<MovieWithDetailsDto> GetMovieByIdAsync(int id)
        {
            var movie = await _unitOfWork.MovieRepository.GetMovieDetailsByIdAsync(id);
            if (movie is null)
            {
                throw new EntityNotFoundException(typeof(Movie), id);
            }

            var movieDetailsDto = _mapper.Map<MovieWithDetailsDto>(movie);
            return movieDetailsDto;
        }

        public async Task<PagedListDto<MovieDto>> GetMoviesAsync(MovieQueryParameters movieQueryParameters)
        {
            var movies = await _unitOfWork.MovieRepository.GetMoviesAsync(movieQueryParameters);
            var moviesDtos = _mapper.Map<PagedListDto<MovieDto>>(movies);
            return moviesDtos;
        }

        public async Task UpdateMovieAsync(int id, MovieUpdateDto movieUpdateDto)
        {
            var movie = await _unitOfWork.MovieRepository.GetMovieDetailsByIdAsync(id);
            if(movie is null)
            {
                throw new EntityNotFoundException(typeof(Movie), id);
            }
            await VerifyActorsExist(movieUpdateDto);
            await VerifyGenresExist(movieUpdateDto.GenresIds);

            _mapper.Map(movieUpdateDto, movie);
            _unitOfWork.MovieRepository.Update(movie);
            await _unitOfWork.SaveChangesAsync();

        }

        public async Task<string> UpdateMovieImageAsync(int id, UpdateImageDto updateImageDto)
        {
            var movie = await _unitOfWork.MovieRepository.GetMovieByIdAsync(id);
            if (movie is null)
            {
                throw new EntityNotFoundException(typeof(Movie), id);
            }

            if (string.IsNullOrEmpty(movie.ImageId))
            {
                var uploadResult = await _uploadFileService.UploadFileAsync(updateImageDto.File, "Movies");
                movie.ImageUrl = uploadResult.Url.ToString();
                movie.ImageId = uploadResult.PublicId;
            }
            else
            {
                var uploadResult = await _uploadFileService.UpdateFileAsync(updateImageDto.File, movie.ImageId);
                movie.ImageUrl = uploadResult.Url.ToString();
            }

            _unitOfWork.MovieRepository.Update(movie);
            await _unitOfWork.SaveChangesAsync();

            return movie.ImageUrl;
        }

        private async Task VerifyActorsExist(MovieUpdateDto movieUpdateDto)
        {
            var actorsIds = movieUpdateDto.Actors.Select(actor => actor.ActorId).ToList();
            var actorsExistCount = await _unitOfWork.ActorRepository.GetByCondition(actor => actorsIds.Contains(actor.Id)).CountAsync();
            if (actorsExistCount != actorsIds.Count)
            {
                throw new BadRequestException("One or more actors were not found.");
            }
        }

        private async Task VerifyGenresExist(List<int> genresIds)
        {
            var genresExistCount = await _unitOfWork.GenreRepository.GetByCondition(genre => genresIds.Contains(genre.Id)).CountAsync();
            if(genresExistCount != genresIds.Count)
            {
                throw new BadRequestException("One or more genres were not found.");
            }
        }

    }

}
