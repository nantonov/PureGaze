using Common.Data.Enums;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Notification.Application.Configurations;
using Notification.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Notification.Infrastructure.Workers;

public abstract class BaseEmailWorker(
    IServiceScopeFactory scopeFactory,
    IOptions<RetryPolicyOptions> options,
    TimeSpan delay,
    EmailPriority priority,
    ILogger<BaseEmailWorker> logger)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await ProcessEmailsAsync(stoppingToken);
            }
            catch
            {
                logger.LogWarning("Failed to process emails");
            }

            await Task.Delay(delay, stoppingToken);
        }
    }

    private async Task ProcessEmailsAsync(CancellationToken cancellationToken)
    {
        using var scope = scopeFactory.CreateScope();
        var emailRepository = scope.ServiceProvider.GetRequiredService<IEmailRepository>();
        var emailSender = scope.ServiceProvider.GetRequiredService<IEmailSender>();

        var maxRetryCount = priority switch
        {
            EmailPriority.High => options.Value.HighPriorityRetryCount,
            EmailPriority.Normal => options.Value.MediumPriorityRetryCount,
            EmailPriority.Low => options.Value.LowPriorityRetryCount,
            _ => 3
        };

        var emails = await emailRepository.GetPendingEmailsAsync(maxRetryCount, priority, cancellationToken);

        foreach (var email in emails)
        {
            email.Status = EmailStatus.Sending;
            await emailRepository.UpdateAsync(email, cancellationToken);

            var success = await emailSender.SendAsync(email, cancellationToken);
            if (success)
            {
                email.Status = EmailStatus.Sent;
                email.SentAt = DateTime.UtcNow;
                email.ErrorMessage = null;
            }
            else
            {
                email.RetryCount++;
                if (email.RetryCount >= maxRetryCount)
                {
                    email.Status = EmailStatus.ExceededRetryCount;
                }
                else
                {
                    email.Status = EmailStatus.Failed;
                }
            }
            await emailRepository.UpdateAsync(email, cancellationToken);
        }
    }
}
