using Common.Data.Enums;
using Common.Domain.Entities;
using Notification.Application.Abstractions.Infrastructure;
using Notification.Application.Abstractions.Services;
using Notification.Application.Contracts.Application;
using Microsoft.Extensions.Logging;

namespace Notification.Application.Services;

public class EmailService(
    IEmailRepository emailRepository,
    IEmailSender emailSender,
    ILogger<EmailService> logger) 
    : IEmailService
{
    public async Task CreateEmailAsync(CreateEmailRequest dto, CancellationToken cancellationToken = default)
    {
        var email = new Email
        {
            Id = Guid.NewGuid(),
            Subject = dto.Subject,
            Body = dto.Body,
            To = dto.To,
            From = "system@example.com",
            RetryCount = 0,
            Status = EmailStatus.InQueue
        };
        
        await emailRepository.AddAsync(email, cancellationToken);
        await emailRepository.SaveChangesAsync(cancellationToken);
    }
    
    public async Task ResendFailedEmailsAsync(CancellationToken cancellationToken = default)
    {
        var emails = await emailRepository.GetExceededEmailsAsync(cancellationToken);
        
        foreach (var email in emails)
        {
            email.RetryCount = 0;
            email.Status = EmailStatus.Sending;
        }
        //to lock em from other
        await emailRepository.SaveChangesAsync(cancellationToken);

        
        foreach (var email in emails)
        {
            email.RetryCount = 0;
            try
            {
                await emailSender.SendAsync(email, cancellationToken);
                email.Status = EmailStatus.Sent;
                email.SentAt = DateTime.UtcNow;
            }
            catch (Exception ex)
            {
                email.Status = EmailStatus.Failed;
                logger.LogError(ex, "Failed to manually resend email {Id}", email.Id);
            }
        }

        await emailRepository.SaveChangesAsync(cancellationToken);
    }

    public Task<List<Email>> GetEmailsAsync(int page, int pageSize, EmailStatus status, CancellationToken cancellationToken = default)
        => emailRepository.GetEmailsAsync(page, pageSize, status, cancellationToken);
}
