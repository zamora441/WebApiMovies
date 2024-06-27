using System.ComponentModel.DataAnnotations;

namespace WebApiMovies.DTOs.AuthDTOs
{
    public class ResetPasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = null!;
        [Required]
        public string Token { get; set; } = null!; 
    }
}
