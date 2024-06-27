namespace WebApiMovies.DTOs.AuthDTOs
{
    public class AuthResponseDto
    {
        public string? Token { get; set; } = null!;
        public bool Is2StepVerificationRequired { get; set; }
        public string? Provider { get; set; }
    }
}
