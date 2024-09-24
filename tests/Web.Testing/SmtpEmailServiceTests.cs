using FluentAssertions;

using Microsoft.Extensions.Options;

using netDumbster.smtp;

using Web.Features.Email;
using Web.Features.Email.SmtpEmail;

namespace Web.Integration.Testing;

public class SmtpEmailServiceTest
{
    [Fact]
    public async Task SmtpEmailService_ShouldThrowAnArgumentNullException_WhenTheRequestIsNull()
    {

        // Arrange
        var emailService = new SmtpEmailService(CreateSmtpEmailServiceOptions(), CreateFormEmailOptions());

        // Act
        var handleSendEmail = () => emailService.SendContactFormAsync(null!, default);

        // Assert
        _ = await handleSendEmail.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task SmtpEmailService_ShouldReturnTrue_WhenItSucceeds()
    {
        // Arrange
        var emailServiceOptions = CreateSmtpEmailServiceOptions();

        using var smtpServer = SimpleSmtpServer.Start(emailServiceOptions.Value.Port);

        var emailService = new SmtpEmailService(CreateSmtpEmailServiceOptions(), CreateFormEmailOptions());
        var sendWebFormRequest = new ContactFormData
        {
            Name = "Bob",
            EmailAddress = "bob@bob.com",
            Message = "Message",
            Subject = "Subject",
        };

        // Act
        var result = await emailService.SendContactFormAsync(sendWebFormRequest, default);

        // Assert
        _ = result.Should().BeTrue();
        _ = smtpServer.ReceivedEmailCount.Should().Be(1);

        // Clean up
        smtpServer.Stop();
    }

    private static IOptions<SmtpEmailServiceOptions> CreateSmtpEmailServiceOptions() =>
        Options.Create(new SmtpEmailServiceOptions
        {
            Password = "smtpPassword",
            Port = 25,
            Host = "localhost",
            Username = "smtpUsername",
        });

    private static IOptions<ContactFormEmailOptions> CreateFormEmailOptions() =>
        Options.Create(new ContactFormEmailOptions
        {
            FromAddress = "kevin@bob.com",
            FromName = "Kevin",
            ToAddress = "kevin@bob.com",
            ToName = "Kevin",
        });
}
