using MimeKit;
using MailKit.Net.Smtp;
using WebApiMovies.DTOs;

namespace WebApiMovies.Services.Implements
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(EmailMessageDto emailMessageDto)
        {
            var emailMessage = CreateEmailMessage(emailMessageDto);
            await SendAsync(emailMessage);
        }

        private MimeMessage CreateEmailMessage(EmailMessageDto emailMessageDto)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(name: null, address: _configuration["EmailUserName"]));
            emailMessage.To.AddRange(emailMessageDto.To);
            emailMessage.Subject = emailMessageDto.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            {
                Text = emailMessageDto.Content
            };

            return emailMessage;
        }

        private async Task SendAsync(MimeMessage emailMessagge)
        {
            using (var client = new SmtpClient())
            {
                var smtpServer = _configuration.GetSection("EmailConfiguration").GetValue<string>("SmtpServer");
                var port = _configuration.GetSection("EmailConfiguration").GetValue<int>("Port");
                var userName = _configuration["EmailUserName"];
                var password = _configuration["EmailPassword"];

                await client.ConnectAsync(smtpServer, port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                await client.AuthenticateAsync(userName, password);

                await client.SendAsync(emailMessagge);

                await client.DisconnectAsync(true);
            }
        }


    }
}
