using MailKit.Net.Smtp;

using Microsoft.Extensions.Options;

using MimeKit;

namespace Web.Features.Email.SmtpEmail;

public sealed class SmtpEmailService(IOptions<SmtpEmailServiceOptions> smtpEmailServiceOptions,
                                     IOptions<ContactFormEmailOptions> contactFormEmailOptions) : IEmailService
{
    public async Task<bool> SendContactFormAsync(ContactFormData contactFormData, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(contactFormData);

        var from = new MailboxAddress(contactFormEmailOptions.Value.FromName, contactFormEmailOptions.Value.FromAddress);
        var to = new MailboxAddress(contactFormEmailOptions.Value.ToName, contactFormEmailOptions.Value.ToAddress);

        var message = new MimeMessage();

        message.From.Add(from);
        message.To.Add(to);
        message.Subject = contactFormData.Subject;
        message.Body = new TextPart("plain") { Text = contactFormData.Message, };

        using var client = new SmtpClient();

        await client.ConnectAsync(smtpEmailServiceOptions.Value.Host, smtpEmailServiceOptions.Value.Port, cancellationToken: cancellationToken);
        await client.AuthenticateAsync(smtpEmailServiceOptions.Value.Username, smtpEmailServiceOptions.Value.Password, cancellationToken: cancellationToken);
        _ = await client.SendAsync(message, cancellationToken: cancellationToken);
        await client.DisconnectAsync(true, cancellationToken: cancellationToken);

        return true;
    }
}