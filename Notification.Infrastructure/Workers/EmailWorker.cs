using Common.Data.Enums;
using Common.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Notification.Application.Abstractions.Infrastructure;

namespace Notification.Infrastructure.Workers;

public sealed class EmailWorker(
    IServiceScopeFactory scopeFactory, 
    IOptions<RetryPolicyOptions> options, 
    ILogger<EmailWorker> logger) 
    : BackgroundWorker(options.Value.DelayInSeconds)
{
    protected override async Task DoWorkAsync(CancellationToken ct)
    {
        using var scope = scopeFactory.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IEmailRepository>();
        var sender = scope.ServiceProvider.GetRequiredService<IEmailSender>();
        
        var emails = await AcquireEmailsAsync(repository, ct);
        
        if (emails.Count == 0) return;
        
        foreach (var email in emails)
        {
            ct.ThrowIfCancellationRequested();
            try
            {
                await sender.SendAsync(email, ct);
                
                email.Status = EmailStatus.Sent;
                email.SentAt = DateTime.UtcNow;
            }
            catch (Exception ex)
            {
                var maxRetryCount = options.Value.MaxRetryCount;
                
                email.RetryCount++;
                email.Status = email.RetryCount >= maxRetryCount
                    ? EmailStatus.ExceededRetryCount
                    : EmailStatus.Failed;
    
                logger.LogError(
                    ex,
                    "Failed to send email {Id}. Retry {Retry}/{MaxRetry}",
                    email.Id,
                    email.RetryCount,
                    maxRetryCount);
            }
        }
        
        await repository.SaveChangesAsync(ct);
    }
    
    private async Task<IList<Email>> AcquireEmailsAsync(
        IEmailRepository repository,
        CancellationToken ct)
    {
        var emails = 
            await repository.GetPendingEmailsAsync(ct);
    
        foreach (var email in emails)
            email.Status = EmailStatus.Sending;
    
        await repository.SaveChangesAsync(ct);
    
        return emails;
    }
} 