using Microsoft.Extensions.Options;

namespace Web.Features.Email;

public sealed class SmtpEmailService : IEmailService
{
    private readonly EmailServiceOptions _emailServiceOptions;

    public SmtpEmailService(IOptions<EmailServiceOptions> emailServiceOptions)
    {
        ArgumentNullException.ThrowIfNull(emailServiceOptions);
        ArgumentNullException.ThrowIfNull(emailServiceOptions.Value);
        ArgumentNullException.ThrowIfNull(emailServiceOptions.Value.SmtpServer);
        ArgumentNullException.ThrowIfNull(emailServiceOptions.Value.SmtpPort);
        ArgumentNullException.ThrowIfNull(emailServiceOptions.Value.SmtpUsername);
        ArgumentNullException.ThrowIfNull(emailServiceOptions.Value.SmtpPassword);

        _emailServiceOptions = emailServiceOptions.Value;
    }

    public async Task<bool> SendEmailAsync(SendWebFormRequest sendWebFormRequest, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(sendWebFormRequest);

        return true;
    }
}
