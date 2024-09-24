using System.ComponentModel.DataAnnotations;

namespace Web.Features.Email;

public sealed class ContactFormData
{
    [Required]
    public string Name { get; set; } = default!;

    [Required, EmailAddress]
    public string EmailAddress { get; set; } = default!;

    [Required]
    public string Subject { get; set; } = default!;

    [Required]
    public string Message { get; set; } = default!;
}