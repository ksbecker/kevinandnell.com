using System.ComponentModel.DataAnnotations;

namespace Web.Features.Email.SmtpEmail;

public sealed class SmtpEmailServiceOptions
{
    public const string SectionName = "SmtpSettings";

    [Required]
    public string Host { get; set; } = default!;
    
    [Required]
    public int Port { get; set; }
    
    [Required]
    public string Username { get; set; } = default!;

    [Required]
    public string Password { get; set; } = default!;
}