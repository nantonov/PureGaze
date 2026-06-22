using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;

namespace PureGaze.Infrastructure.Notifications.Emails;

public class EmailSender : IEmailSender, IDisposable
{
    private readonly ILogger<EmailSender> logger;
    private readonly bool enabled;
    private readonly MailAddress email;
    private readonly SmtpClient smtpClient;

    public EmailSender(IOptions<SmtpOptions> options, ILogger<EmailSender> logger)
    {
        this.logger = logger;

        enabled = options.Value.Enabled;
        email = new MailAddress(options.Value.Email);

        smtpClient = new SmtpClient
        {
            Host = options.Value.Host,
            Port = options.Value.Port,
            Credentials = new NetworkCredential(options.Value.Email, options.Value.Password),
            EnableSsl = true,
        };
    }

    public void Dispose()
    {
        smtpClient.Dispose();
    }

    public async Task SendAsync(Email email, CancellationToken ct = default)
    {
        if (!enabled)
        {
            logger.LogWarning("Email sending is disabled for development. If you want to enable it check Smtp__Enabled option.");
            return;
        }

        MailMessage mailMessage = new MailMessage
        {
            From = this.email,
            Subject = email.Subject,
            Body = email.Body,
            IsBodyHtml = false
        };

        if (email.To == null)
            throw new ValidationException("Target email address is required");

        mailMessage.To.Add(email.To);

        await smtpClient.SendMailAsync(mailMessage, ct);
    }
}
