namespace Web.Features.Email;

public sealed class EmailServiceOptions
{
    public const string EmailService = "EmailService";

    public string SmtpServer { get; set; } = default!;
    public int SmtpPort { get; set; }
    public string SmtpUsername { get; set; } = default!;
    public string SmtpPassword { get; set; } = default!;
}