using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using WebApiMovies.DTOs;
using WebApiMovies.DTOs.MovieDTOs;
using WebApiMovies.Parameters;
using WebApiMovies.Services;

namespace WebApiMovies.Controllers
{
    [Route("api/movies")]
    [Authorize(Roles = "Administrator")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            this._movieService = movieService;
        }

        [AllowAnonymous]
        [HttpGet(Name = "GetMovies")]
        public async Task<ActionResult<PagedListDto<MovieDto>>> Get([FromQuery] MovieQueryParameters movieQueryParameters)
        {
            var moviesDtos = await _movieService.GetMoviesAsync(movieQueryParameters);
            return moviesDtos;
        }

        [AllowAnonymous]
        [HttpGet("{id:int}", Name ="GetMovieById")]
        public async Task<ActionResult<MovieWithDetailsDto>> GetById([FromRoute] int id)
        {
            var movieWithDetailsDto = await _movieService.GetMovieByIdAsync(id);
            return movieWithDetailsDto;
        }


        [HttpPost(Name ="PostMovie")]
        public async Task<ActionResult> Post([FromForm] MovieCreateDto movieCreateDto)
        {
            var movieDto = await _movieService.CreateMovieAsync(movieCreateDto);
            return CreatedAtAction("GetById", new { id = movieDto.Id }, movieDto);
        }

        [HttpPut("{id:int}", Name = "PutMovie")]
        public async Task<ActionResult> Put([FromRoute] int id, [FromBody] MovieUpdateDto movieUpdateDto)
        {
            await _movieService.UpdateMovieAsync(id, movieUpdateDto);
            return NoContent();
        }

        [HttpPatch("{id:int}", Name ="UpdateMovieImage")]
        public async Task<string> Patch([FromRoute] int id, [FromForm] UpdateImageDto updateImageDto)
        {
            var imageUrl = await _movieService.UpdateMovieImageAsync(id, updateImageDto);
            return imageUrl;
        }

        [HttpDelete("{id:int}", Name ="DeleteMovie")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await _movieService.DeleteMovieAsync(id);
            return NoContent();
        }

    }
}
