using System.Net.Mail;

namespace Web.Features.Email;

public class SmtpClientGenerator : ISmtpClientGenerator
{
    public SmtpClient GenerateSmtpClient()
    {
        throw new NotImplementedException();
    }
}

public interface ISmtpClientGenerator
{
    SmtpClient GenerateSmtpClient();
}