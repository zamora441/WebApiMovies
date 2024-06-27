using Microsoft.AspNetCore.Identity;

namespace WebApiMovies.Data.Entities
{
    public class User : IdentityUser
    {
        public ICollection<MovieReview> MovieReviews { get; set; } = new List<MovieReview>();
    }
}
