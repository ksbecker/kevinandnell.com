using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using OneOf;
using OneOf.Types;
using Web.Domain;

namespace Web.Features.ContactForm.SmtpEmail;

public sealed class SmtpEmailService(IOptions<SmtpEmailServiceOptions> smtpEmailServiceOptions,
                                     IOptions<ContactFormEmailOptions> contactFormEmailOptions) : IEmailService
{
    public async Task<OneOf<Success, Error>> SendContactFormAsync(ContactFormData contactFormData, CancellationToken cancellationToken)
    {
        if (contactFormData is null)
            return new Error();

        try
        {
            var from = new MailboxAddress(contactFormEmailOptions.Value.FromName, contactFormEmailOptions.Value.FromAddress);
            var to = new MailboxAddress(contactFormEmailOptions.Value.ToName, contactFormEmailOptions.Value.ToAddress);

            var message = new MimeMessage();

            message.From.Add(from);
            message.To.Add(to);
            message.Subject = contactFormData.Subject;
            message.Body = new TextPart("plain") { Text = contactFormData.EmailMessage, };
            
            using var client = new SmtpClient();

            await client.ConnectAsync(smtpEmailServiceOptions.Value.Host, smtpEmailServiceOptions.Value.Port, cancellationToken: cancellationToken);
            await client.AuthenticateAsync(smtpEmailServiceOptions.Value.Username, smtpEmailServiceOptions.Value.Password, cancellationToken: cancellationToken);
            _ = await client.SendAsync(message, cancellationToken: cancellationToken);
            await client.DisconnectAsync(true, cancellationToken: cancellationToken);
        }
        catch (Exception)
        {
            return new Error();
        }

        return new Success();
    }
}