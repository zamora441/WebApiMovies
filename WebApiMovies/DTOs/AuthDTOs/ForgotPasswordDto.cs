using System.ComponentModel.DataAnnotations;

namespace WebApiMovies.DTOs.AuthDTOs
{
    public class ForgotPasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string ClientUri { get; set; } = null!;

    }
}
