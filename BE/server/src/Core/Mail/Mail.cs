namespace Core.Mail;

using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.Threading.Tasks;
public interface IMailService
{
  Task SendEmailAsync(string email, string subject, string message);
}

public class MailService : IMailService
{
  private readonly string _smtpServer;
  private readonly int _smtpPort;
  private readonly string _smtpUsername;
  private readonly string _smtpPassword;

  public MailService(string smtpServer, int smtpPort, string smtpUsername, string smtpPassword)
  {
    _smtpServer = smtpServer;
    _smtpPort = smtpPort;
    _smtpUsername = smtpUsername;
    _smtpPassword = smtpPassword;
  }

  public async Task SendEmailAsync(string email, string subject, string message)
  {
    var mimeMessage = new MimeMessage();
    mimeMessage.From.Add(new MailboxAddress("DotNet Template", _smtpUsername));
    mimeMessage.To.Add(new MailboxAddress("Receiver Name", email));

    mimeMessage.Subject = subject;

    var bodyBuilder = new BodyBuilder
    {
      HtmlBody = message
    };

    mimeMessage.Body = bodyBuilder.ToMessageBody();

    using (var client = new SmtpClient())
    {
      await client.ConnectAsync(_smtpServer, _smtpPort, SecureSocketOptions.StartTls);
      await client.AuthenticateAsync(_smtpUsername, _smtpPassword);
      await client.SendAsync(mimeMessage);
      await client.DisconnectAsync(true);
    }
  }
}
