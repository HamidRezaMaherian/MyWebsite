namespace MyWebsite.Application.Utilities
{
	public interface IEmailService
	{
		Task SendEmailAsync(string email, string subject, string htmlMessage);
	}
}
