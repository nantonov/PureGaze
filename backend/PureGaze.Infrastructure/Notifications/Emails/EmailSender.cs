using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Domain.Entities;

namespace PureGaze.Infrastructure.Notifications.Emails;

public class EmailSender(IOptions<SmtpOptions> options) : IEmailSender
{
    private readonly SmtpOptions _options = options.Value;

    public async Task SendAsync(Email email, CancellationToken ct = default)
    {
        var mailMessage = new MailMessage
        {
            From = new MailAddress(_options.Email),
            Subject = email.Subject,
            Body = email.Body,
            IsBodyHtml = false
        };
        
        mailMessage.To.Add(email.To);

        using var client = new SmtpClient();
        client.Host = _options.Host;
        client.Port = _options.Port;
        client.Credentials = new NetworkCredential(_options.Email, _options.Password);
        client.EnableSsl = _options.EnableSsl;
        
        await client.SendMailAsync(mailMessage, ct);
    }
}
