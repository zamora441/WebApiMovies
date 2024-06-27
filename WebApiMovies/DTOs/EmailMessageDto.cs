using MimeKit;

namespace WebApiMovies.DTOs
{
    public class EmailMessageDto
    {
        public List<MailboxAddress> To { get; set; } = new List<MailboxAddress>();
        public string Subject { get; set; } = null!;
        public string Content { get; set; } = null!;

        public EmailMessageDto(List<string> to, string subject, string content)
        {
            To = To = to.Select(x => new MailboxAddress(name: null, address: x)).ToList();
            Subject = subject;
            Content = content;
        }
    }
}
