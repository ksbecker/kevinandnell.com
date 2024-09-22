namespace Web.Features.Email;

public sealed record SendWebFormRequest(string[] SendEmailsTo, string Subject, string Body);
