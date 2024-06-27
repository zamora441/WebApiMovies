using WebApiMovies.DTOs;
using WebApiMovies.DTOs.ActorDTOs;
using WebApiMovies.Parameters;

namespace WebApiMovies.Services
{
    public interface IActorService
    {
        Task<PagedListDto<ActorDto>> GetActorsAsync(ActorQueryParameters actorQueryParameters);
        Task<ActorWithMoviesDto> GetActorByIdAsync(int id);
        Task<ActorDto> CreateActorAsync(ActorCreateDto actorCreateDto);
        Task UpdateActorAsync(int id, ActorUpdateDto actorUpdateDto);
        Task DeleteActorAsync(int id);
        Task<string> UpdateActorImageAsync(int id, UpdateImageDto updateFileDto);
    }
}
