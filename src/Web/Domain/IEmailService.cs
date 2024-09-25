using OneOf;
using OneOf.Types;
using Web.Features.ContactForm;

namespace Web.Domain;

public interface IEmailService
{
    Task<OneOf<Success, Error>> SendContactFormAsync(ContactFormData sendWebFormRequest, CancellationToken cancellationToken);
}