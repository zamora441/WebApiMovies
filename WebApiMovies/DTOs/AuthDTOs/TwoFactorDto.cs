using System.ComponentModel.DataAnnotations;

namespace WebApiMovies.DTOs.AuthDTOs
{
    public class TwoFactorDto
    {
        [Required]
        public string UserName { get; set; } = null!;
        [Required]
        public string Provider { get; set; } = null!;
        [Required]
        public string Token { get; set; } = null!;
    }
}
