using System.ComponentModel.DataAnnotations;

namespace Web.Features.Email.SmtpEmail;

public class ContactFormEmailOptions
{
    public const string SectionName = "ContactFormEmailSettings";

    [Required]
    public string FromName { get; set; } = default!;

    [Required, EmailAddress]
    public string FromAddress { get; set; } = default!;

    [Required]
    public string ToName { get; set; } = default!;
    
    [Required, EmailAddress]
    public string ToAddress { get; set; } = default!;
}