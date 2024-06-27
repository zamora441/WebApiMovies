using System.ComponentModel.DataAnnotations;

namespace WebApiMovies.DTOs.AuthDTOs
{
    public class ConfirmEmailDto
    {
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Token { get; set; } = null!;
    }
}
