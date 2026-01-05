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
    public async Task CreateEmailAsync(CreateEmailRequest request, CancellationToken ct = default)
    {
        await emailRepository.AddAsync(new Email
        {
            Id = Guid.NewGuid(),
            Subject = request.Subject,
            Body = request.Body,
            To = request.To,
            From = "system@example.com",
            RetryCount = 0,
            Status = EmailStatus.InQueue
        }, ct);
        
        await emailRepository.SaveChangesAsync(ct);
    }
    
    public async Task ResendEmailManuallyAsync(Guid id, CancellationToken ct = default)
    {
        var email = await emailRepository.GetByIdAsync(id, ct);
        if (email is null)
            throw new KeyNotFoundException($"Email with id {id} not found");
        
        try
        {
            await emailSender.SendAsync(email, ct);
            
            email.Status = EmailStatus.Sent;
            email.SentAt = DateTime.UtcNow;
        }
        catch (Exception e)
        { 
            //TODO: log exception    
        }
        
        email.UpdatedAt = DateTime.UtcNow;
        email.RetryCount++;
        
        await emailRepository.SaveChangesAsync(ct);
        
        //TODO: if exception we should return info message
    }

    public Task<IList<Email>> GetEmailsAsync(int page, int pageSize, EmailStatus status, CancellationToken ct = default)
        => emailRepository.GetEmailsAsync(page, pageSize, status, ct);
}
