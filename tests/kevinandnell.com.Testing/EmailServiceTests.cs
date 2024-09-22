using FluentAssertions;
using Microsoft.Extensions.Options;
using Web.Features.Email;

namespace Web.Integration.Testing;

public class EmailServiceTest
{
    [Fact]
    public async Task ServiceShouldThrowAnArgumentNullExceptionIfTheRequestIsNull()
    {
        // Arrange
        var emailService = new SmtpEmailService(CreateEmailServiceOptions());

        // Act
        var handleSendEmail = () => emailService.SendEmailAsync(null!, default);

        // Assert
        await handleSendEmail.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task ServiceShouldReturnTrueIfItSucceeds()
    {
        // Arrange
        var emailService = new SmtpEmailService(CreateEmailServiceOptions());
        var sendWebFormRequest = new SendWebFormRequest(null!, null!, null!);

        // Act
        var result = await emailService.SendEmailAsync(sendWebFormRequest, default);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void ServiceShouldThrowAnArgumentNullExceptionWhenOptionsAreNull()
    {
        // Act
        var result = () => new SmtpEmailService(null!);

        // Assert
        result.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void ServiceShouldThrowAnArgumentNullExceptionWhenSmtpPasswordIsNull()
    {
        // Arrange
        var emailServiceOptions = Options.Create(new EmailServiceOptions
        {
            SmtpPassword = null!,
            SmtpPort = 123,
            SmtpServer = "smtpServer",
            SmtpUsername = "smtpUsername",
        });

        // Act
        var result = () => new SmtpEmailService(emailServiceOptions);

        // Assert
        result.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void ServiceShouldThrowAnArgumentNullExceptionWhenSmtpServerIsNull()
    {
        // Arrange
        var emailServiceOptions = Options.Create(new EmailServiceOptions
        {
            SmtpPassword = "smtpPassword",
            SmtpPort = 123,
            SmtpServer = null!,
            SmtpUsername = "smtpUsername",
        });

        // Act
        var result = () => new SmtpEmailService(emailServiceOptions);

        // Assert
        result.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void ServiceShouldThrowAnArgumentNullExceptionWhenSmtpUsernameIsNull()
    {
        // Arrange
        var emailServiceOptions = Options.Create(new EmailServiceOptions
        {
            SmtpPassword = "smtpPassword",
            SmtpPort = 123,
            SmtpServer = "smtpServer",
            SmtpUsername = null!,
        });

        // Act
        var result = () => new SmtpEmailService(emailServiceOptions);

        // Assert
        result.Should().Throw<ArgumentNullException>();
    }

    private static IOptions<EmailServiceOptions> CreateEmailServiceOptions() =>
        Options.Create(new EmailServiceOptions
        {
            SmtpPassword = "smtpPassword",
            SmtpPort = 123,
            SmtpServer = "smtpServer",
            SmtpUsername = "smtpUsername",
        });
}