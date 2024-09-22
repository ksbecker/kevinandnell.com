namespace Web.Features.Email;

public interface IEmailService
{
    Task<bool> SendEmailAsync(SendWebFormRequest sendWebFormRequest, CancellationToken cancellationToken);
}
