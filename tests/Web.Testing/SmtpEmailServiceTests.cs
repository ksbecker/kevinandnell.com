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

    [Fact]
    public void SmtpEmailService_ShouldThrowAnArgumentNullException_WhenSmtpServerOptionsAreNull()
    {
        // Act
        var result = () => new SmtpEmailService(null!, CreateFormEmailOptions());

        // Assert
        _ = result.Should().Throw<ArgumentNullException>();
    }

    [Theory]
    [InlineData(null, 123, "localhost", "smtpUsername")]
    [InlineData("smtpPassword", 123, null, "smtpUsername")]
    [InlineData("smtpPassword", 123, "localhost", null)]
    public void SmtpEmailService_ShouldThrowAnArgumentNullException_WhenAnOptionValueIsNull(string password, int port, string server, string username)
    {
        // Arrange
        var emailServiceOptions = Options.Create(new SmtpEmailServiceOptions
        {
            Password = password,
            Port = port,
            Host = server,
            Username = username,
        });

        // Act
        var result = () => new SmtpEmailService(emailServiceOptions, CreateFormEmailOptions());

        // Assert
        _ = result.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void SmtpEmailService_ShouldThrowAnArgumentNullException_WhenContactFormOptionsAreNull()
    {
        // Act
        var result = () => new SmtpEmailService(CreateSmtpEmailServiceOptions(), null!);

        // Assert
        _ = result.Should().Throw<ArgumentNullException>();
    }

    [Theory]
    [InlineData(null, "FromName", "ToAddress", "ToName")]
    [InlineData("FromAddress", null, "ToAddress", "ToName")]
    [InlineData("FromAddress", "FromName", null, "ToName")]
    [InlineData("FromAddress", "FromName", "ToAddress", null)]
    public void SmtpEmailService_ShouldThrowAnArgumentNullException_WhenContactFormOptionValueIsNull(string fromAddress, string fromName, string toAddress, string toName)
    {
        // Arrange
        var emailServiceOptions = Options.Create(new ContactFormEmailOptions
        {
            FromAddress = fromAddress,
            FromName = fromName,
            ToAddress = toAddress,
            ToName = toName,
        });

        // Act
        var result = () => new SmtpEmailService(CreateSmtpEmailServiceOptions(), emailServiceOptions);

        // Assert
        _ = result.Should().Throw<ArgumentNullException>();
    }

    private static IOptions<SmtpEmailServiceOptions> CreateSmtpEmailServiceOptions() =>
        Options.Create(new SmtpEmailServiceOptions
        {
            Password = "smtpPassword",
            Port = 123,
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