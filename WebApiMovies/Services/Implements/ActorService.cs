using AutoMapper;
using WebApiMovies.Data.Entities;
using WebApiMovies.Data_Access;
using WebApiMovies.DTOs;
using WebApiMovies.DTOs.ActorDTOs;
using WebApiMovies.Exceptions;
using WebApiMovies.Parameters;

namespace WebApiMovies.Services.Implements
{
    public sealed class ActorService : IActorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUploadFileService _uploadFileService;

        public ActorService(IUnitOfWork unitOfWork, IMapper mapper, IUploadFileService uploadFileService)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._uploadFileService = uploadFileService;
        }
        public async Task<ActorDto> CreateActorAsync(ActorCreateDto actorCreateDto)
        {
            var countryExists = await _unitOfWork.CountryRepository.GetCountryByIdAsync(actorCreateDto.CountryId);
            if(countryExists is null)
            {
                throw new BadRequestException($"The country with the id {actorCreateDto.CountryId} was not found");
            }

            var actor = _mapper.Map<Actor>(actorCreateDto);

            if(actorCreateDto.ActorImage is not null)
            {
                var resultUpload = await _uploadFileService.UploadFileAsync(actorCreateDto.ActorImage, "Actors");
                actor.ImageUrl = resultUpload.Url.ToString();
                actor.ImageId = resultUpload.PublicId;
            }

            _unitOfWork.ActorRepository.Create(actor);
            await _unitOfWork.SaveChangesAsync();
            var actorDto = _mapper.Map<ActorDto>(actor);
            actorDto.CountryName = countryExists.Name;
            return actorDto;
        }

        public async Task DeleteActorAsync(int id)
        {
            var actor = await VerifyActorExists(id);

            _unitOfWork.ActorRepository.Delete(actor);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<ActorWithMoviesDto> GetActorByIdAsync(int id)
        {
            var actor = await VerifyActorExists(id);

            var actorDto = _mapper.Map<ActorWithMoviesDto>(actor);
            return actorDto;
        }

        public async Task<PagedListDto<ActorDto>> GetActorsAsync(ActorQueryParameters actorQueryParameters)
        {
            var actors = await _unitOfWork.ActorRepository.GetActorsAsync(actorQueryParameters);
            var actorsDtos = _mapper.Map<PagedListDto<ActorDto>>(actors);
            return actorsDtos;
        }

        public async Task UpdateActorAsync(int id, ActorUpdateDto actorUpdateDto)
        {
            var countryExists = await _unitOfWork.CountryRepository.GetCountryByIdAsync(actorUpdateDto.CountryId);
            if (countryExists is null)
            {
                throw new BadRequestException($"The country with the id {actorUpdateDto.CountryId} was not found in the system.");
            }

            var actor = await VerifyActorExists(id);

            _mapper.Map(actorUpdateDto, actor);
           
            _unitOfWork.ActorRepository.Update(actor);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<string> UpdateActorImageAsync(int id, UpdateImageDto updateImageDto)
        {
            var actor = await VerifyActorExists(id);
            if (string.IsNullOrEmpty(actor.ImageId))
            {
                var resultUpload = await _uploadFileService.UploadFileAsync(updateImageDto.File, "Actors");
                actor.ImageUrl = resultUpload.Url.ToString();
                actor.ImageId = resultUpload.PublicId;
            }
            else
            {
                var resultUpload = await _uploadFileService.UpdateFileAsync(updateImageDto.File, actor.ImageId);
                actor.ImageUrl = resultUpload.Url.ToString();
            }


            _unitOfWork.ActorRepository.Update(actor);
            await _unitOfWork.SaveChangesAsync();

            return actor.ImageUrl;
        }

        private async Task<Actor> VerifyActorExists(int id)
        {
            var actorExists = await _unitOfWork.ActorRepository.GetActorByIdAsync(id);
            if(actorExists is null)
            {
                throw new EntityNotFoundException(typeof(Actor), id);
            }

            return actorExists;
        }
    }
}
