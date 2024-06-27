using System.ComponentModel.DataAnnotations;

namespace WebApiMovies.DTOs.AuthDTOs
{
    public class UserLoginDto
    {
        [Required]
        public string UserName { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public string ClientUri { get; set; } = null!;
    }
}
