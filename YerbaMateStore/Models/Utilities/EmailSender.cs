using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;

namespace YerbaMateStore.Models.Utilities;

public class EmailSender : IEmailSender
{
  private readonly EmailSenderAccessData _emailAccessData;

  public EmailSender(EmailSenderAccessData emailAccessData)
  {
    _emailAccessData = emailAccessData;
  }

  public Task SendEmailAsync(string email, string subject, string htmlMessage)
  {    
    var emailToSend = new MimeMessage();
    emailToSend.From.Add(MailboxAddress.Parse(_emailAccessData.EmailAddress));
    emailToSend.To.Add(MailboxAddress.Parse(email));
    emailToSend.Subject = subject;
    emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };


    //send email
    using (var emailClient = new SmtpClient())
    {
      emailClient.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
      emailClient.Authenticate(_emailAccessData.EmailAddress, _emailAccessData.EmailAccessToken);
      emailClient.Send(emailToSend);
      emailClient.Disconnect(true);
    }


    return Task.CompletedTask;
  }
}
