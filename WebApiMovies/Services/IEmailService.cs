using WebApiMovies.DTOs;

namespace WebApiMovies.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailMessageDto emailMessageDto);
    }
}
