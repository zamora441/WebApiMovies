using Microsoft.AspNetCore.Mvc;
using WebApiMovies.DTOs;
using WebApiMovies.Services;

namespace WebApiMovies.Controllers
{
    [Route("api/genres")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;
        private readonly ILogger<GenreController> logger;

        public GenreController(IGenreService genreService, ILogger<GenreController> logger)
        {
            this._genreService = genreService;
            this.logger = logger;
        }

        [HttpGet(Name = "GetGenres")]
        public async Task<ActionResult<List<GenreDto>>> Get()
        {
            logger.LogInformation("progando esta vrg");
            var genresDtos = await _genreService.GetGenresAsync();
            return Ok(genresDtos);
        }
    }
}
