namespace Web.Features.Email;

public interface IEmailService
{
    Task<bool> SendContactFormAsync(ContactFormData sendWebFormRequest, CancellationToken cancellationToken);
}
