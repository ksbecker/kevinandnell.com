using System.ComponentModel.DataAnnotations;

namespace Web.Features.ContactForm;

public sealed class ContactFormData
{
    [Required]
    public string Name { get; set; } = default!;

    [Required(ErrorMessage = "The Email Address field is required."), EmailAddress]
    public string EmailAddress { get; set; } = default!;

    [Required]
    public string Subject { get; set; } = default!;

    [Required]
    public string Message { get; set; } = default!;

    public string EmailMessage => $"From:\r\n{Name}\r\n{EmailAddress}\r\n\r\n{Message}";
}