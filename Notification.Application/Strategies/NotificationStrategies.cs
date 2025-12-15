using Common.Domain.Entities;
using Common.Domain.Enums;
using Notification.Application.Interfaces;

namespace Notification.Application.Strategies;

public class HighPriorityNotificationStrategy : INotificationStrategy
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
            // Logging can be added here if needed, but user requested minimal logs.
        }

        if (sent)
        {
            email.Status = EmailStatus.Sent;
            email.SentAt = DateTime.UtcNow;
            await repository.AddAsync(email, cancellationToken);
        }
        else
        {
            email.Status = EmailStatus.Failed; // Or InQueue? User said "save to db with RetryCount = 1". Status naming implies failure of first attempt.
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
