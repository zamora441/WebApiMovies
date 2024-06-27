using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApiMovies.DTOs;
using WebApiMovies.DTOs.MovieReviewsDTOs;
using WebApiMovies.HttpParameters;
using WebApiMovies.Services;

namespace WebApiMovies.Controllers
{
    [ApiController]
    [Authorize(Roles = "Administrator, User")]
    [Route("api/movies/{movieId:int}/reviews")]
    public class MovieReviewController : ControllerBase
    {
        private readonly IMovieReviewService _movieReviewService;

        public MovieReviewController(IMovieReviewService movieReviewService)
        {
            this._movieReviewService = movieReviewService;
        }

        [AllowAnonymous]
        [HttpGet(Name ="GetMovieReviews")]
        public async Task<ActionResult<PagedListDto<MovieReviewDto>>> Get([FromRoute] int movieId , [FromQuery] MovieReviewQueryParameters movieReviewQueryParameters)
        {
            var movieReviewsDtos = await _movieReviewService.GetMovieReviewsAsync(movieId, movieReviewQueryParameters);
            return movieReviewsDtos;
        }

        [HttpPost(Name ="PostMovieReview")]
        public async Task<ActionResult> Post([FromRoute] int movieId, [FromBody] MovieReviewCreateDto movieReviewCreateDto)
        {
            var userId = User.FindFirstValue("userId");
            var movieReviewDto = await _movieReviewService.CreateMovieReviewAsync(movieId, userId ,movieReviewCreateDto);
            return CreatedAtAction(null, movieReviewDto);
        }

        [HttpPut("{id:int}",Name = "PutMovieReview")]
        public async Task<ActionResult> Put([FromRoute] int id, [FromBody] MovieReviewCreateDto movieReviewCreateDto)
        {
            var userId = User.FindFirstValue("userId");
            await _movieReviewService.UpdateMovieReviewAsync(id, userId, movieReviewCreateDto);
            return NoContent();
        }

        [HttpDelete("{id:int}",Name = "DeleteMovieReview")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var userId = User.FindFirstValue("userId");
            await _movieReviewService.DeleteMovieReviewAsync(id, userId);
            return NoContent();
        }
    }
}
