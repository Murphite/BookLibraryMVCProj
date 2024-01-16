namespace BookLibrarySoln.Services.Interface
{
    public interface IEmailService
    {
        Task<string> SendEmailAsync(string recipientEmail, string subject, string body);

    }
}
