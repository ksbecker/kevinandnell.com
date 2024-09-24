using MailKit.Net.Smtp;

using Microsoft.Extensions.Options;

using MimeKit;

namespace Web.Features.Email.SmtpEmail;

public sealed class SmtpEmailService : IEmailService
{
    private readonly IOptions<SmtpEmailServiceOptions> _smtpEmailServiceOptions;
    private readonly IOptions<ContactFormEmailOptions> _contactFormEmailOptions;

    public SmtpEmailService(IOptions<SmtpEmailServiceOptions> smtpEmailServiceOptions, IOptions<ContactFormEmailOptions> contactFormEmailOptions)
    {
        ArgumentNullException.ThrowIfNull(smtpEmailServiceOptions);
        ArgumentNullException.ThrowIfNull(smtpEmailServiceOptions.Value);
        ArgumentNullException.ThrowIfNull(smtpEmailServiceOptions.Value.Host);
        ArgumentNullException.ThrowIfNull(smtpEmailServiceOptions.Value.Port);
        ArgumentNullException.ThrowIfNull(smtpEmailServiceOptions.Value.Username);
        ArgumentNullException.ThrowIfNull(smtpEmailServiceOptions.Value.Password);

        ArgumentNullException.ThrowIfNull(contactFormEmailOptions);
        ArgumentNullException.ThrowIfNull(contactFormEmailOptions.Value);
        ArgumentNullException.ThrowIfNull(contactFormEmailOptions.Value.FromName);
        ArgumentNullException.ThrowIfNull(contactFormEmailOptions.Value.FromAddress);
        ArgumentNullException.ThrowIfNull(contactFormEmailOptions.Value.ToName);
        ArgumentNullException.ThrowIfNull(contactFormEmailOptions.Value.ToAddress);

        _smtpEmailServiceOptions = smtpEmailServiceOptions;
        _contactFormEmailOptions = contactFormEmailOptions;
    }

    public async Task<bool> SendContactFormAsync(ContactFormData contactFormData, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(contactFormData);

        var from = new MailboxAddress(_contactFormEmailOptions.Value.FromName, _contactFormEmailOptions.Value.FromAddress);
        var to = new MailboxAddress(_contactFormEmailOptions.Value.ToName, _contactFormEmailOptions.Value.ToAddress);

        var message = new MimeMessage();

        message.From.Add(from);
        message.To.Add(to);
        message.Subject = contactFormData.Subject;
        message.Body = new TextPart("plain") { Text = contactFormData.Message, };

        using var client = new SmtpClient();

        await client.ConnectAsync(_smtpEmailServiceOptions.Value.Host, _smtpEmailServiceOptions.Value.Port, cancellationToken: cancellationToken);
        await client.AuthenticateAsync(_smtpEmailServiceOptions.Value.Username, _smtpEmailServiceOptions.Value.Password, cancellationToken: cancellationToken);
        _ = await client.SendAsync(message, cancellationToken: cancellationToken);
        await client.DisconnectAsync(true, cancellationToken: cancellationToken);

        return true;
    }
}