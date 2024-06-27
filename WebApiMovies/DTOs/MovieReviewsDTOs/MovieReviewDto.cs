using System.ComponentModel.DataAnnotations;
using WebApiMovies.Data.Entities;

namespace WebApiMovies.DTOs.MovieReviewsDTOs
{
    public class MovieReviewDto
    {
        public int Id { get; set; }
        public string Review { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string UserUserName { get; set; } = null!;

    }
}
