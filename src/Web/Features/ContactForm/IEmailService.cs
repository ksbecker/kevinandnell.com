namespace Web.Features.ContactForm;

public interface IEmailService
{
    Task<bool> SendContactFormAsync(ContactFormData sendWebFormRequest, CancellationToken cancellationToken);
}