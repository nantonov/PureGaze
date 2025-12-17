using Common.Domain.Entities;
using Common.Domain.Enums;
using Microsoft.Extensions.Logging;
using Notification.Application.Interfaces;

namespace Notification.Application.Strategies;

public class HighPriorityNotificationStrategy(ILogger<HighPriorityNotificationStrategy> logger) : INotificationStrategy
{
    public async Task ProcessAsync(Email email, IEmailRepository repository, IEmailSender sender, CancellationToken cancellationToken = default)
    {
        bool sent = false;
        try
        {
            sent = await sender.SendAsync(email, cancellationToken);
        }
        catch
        {
            logger.LogWarning("Failed to send high priority email");
        }

        if (sent)
        {
            email.Status = EmailStatus.Sent;
            email.SentAt = DateTime.UtcNow;
            await repository.AddAsync(email, cancellationToken);
        }
        else
        {
            email.Status = EmailStatus.Failed; 
            email.RetryCount = 1;
            await repository.AddAsync(email, cancellationToken);
        }
    }
}

public class StandardNotificationStrategy : INotificationStrategy
{
    public async Task ProcessAsync(Email email, IEmailRepository repository, IEmailSender sender, CancellationToken cancellationToken = default)
    {
        email.Status = EmailStatus.InQueue;
        await repository.AddAsync(email, cancellationToken);
    }
}
