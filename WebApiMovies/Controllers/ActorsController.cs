using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiMovies.DTOs;
using WebApiMovies.DTOs.ActorDTOs;
using WebApiMovies.Parameters;
using WebApiMovies.Services;

namespace WebApiMovies.Controllers
{
    [Route("api/actors")]
    [Authorize(Roles = "Administrator")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly IActorService _actorService;

        public ActorsController(IActorService actorService)
        {
            this._actorService = actorService;
        }

        [HttpGet(Name = "GetActors")]
        [AllowAnonymous]
        public async Task<ActionResult<PagedListDto<ActorDto>>> Get([FromQuery] ActorQueryParameters actorQueryParameters)
        {
            var actorsDtos = await _actorService.GetActorsAsync(actorQueryParameters);
            return actorsDtos;
        }

        [HttpGet("{id:int}", Name = "GetActorById")]
        [AllowAnonymous]
        public async Task<ActionResult<ActorWithMoviesDto>> GetById([FromRoute] int id)
        {
            var actorWithMoviesDto = await _actorService.GetActorByIdAsync(id);
            return actorWithMoviesDto;
        }

        [HttpPost(Name = "PostActor")]
        public async Task<ActionResult> Post([FromForm] ActorCreateDto actorCreateDto)
        {
            var actorDto = await _actorService.CreateActorAsync(actorCreateDto);
            return CreatedAtAction("GetById", new { id = actorDto.Id }, actorDto);
        }


        [HttpPut("{id:int}", Name = "PutActor")]
        public async Task<ActionResult> Put([FromRoute] int id, [FromBody] ActorUpdateDto actorUpdateDto)
        {
            await _actorService.UpdateActorAsync(id, actorUpdateDto);

            return NoContent();
        }

        [HttpPatch("{id:int}", Name = "UpdateActorImage")]
        public async Task<string> Patch([FromRoute] int id, [FromForm] UpdateImageDto updateImageDto)
        {
            var imageUrl = await _actorService.UpdateActorImageAsync(id, updateImageDto);
            return imageUrl;
        }

        [HttpDelete("{id:int}", Name ="DeleteActor")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await _actorService.DeleteActorAsync(id);
            return NoContent();
        }
    }
}
