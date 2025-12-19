using Common.Data.Enums;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Notification.Application.Contracts;
using Common.Domain.Entities;

namespace Notification.Infrastructure.Workers;

public class EmailWorker(
    IServiceScopeFactory scopeFactory,
    IOptions<RetryPolicyOptions> options,
    ILogger logger,
    EmailPriority priority,
    TimeSpan delay)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        logger.LogInformation(
            "Email worker started for {Priority} priority with {Delay} delay",
            priority,
            delay);

        while (!ct.IsCancellationRequested)
        {
            try
            {
                await ProcessEmailsAsync(ct);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unhandled error in email worker for {Priority}", priority);
            }

            await Task.Delay(delay, ct);
        }
    }

    private async Task ProcessEmailsAsync(CancellationToken ct)
    {
        using var scope = scopeFactory.CreateScope();
        var repo = scope.ServiceProvider.GetRequiredService<IEmailRepository>();
        var sender = scope.ServiceProvider.GetRequiredService<IEmailSender>();

        var maxRetryCount = GetMaxRetryCount();
        var emails = await AcquireEmailsAsync(repo, maxRetryCount, ct);

        if (emails.Count == 0)
        {
            logger.LogDebug("No emails to process for {Priority}", priority);
            return;
        }

        logger.LogInformation(
            "Processing {Count} emails with {Priority} priority",
            emails.Count,
            priority);

        var failedEmails = new List<Email>();

        foreach (var email in emails)
        {
            ct.ThrowIfCancellationRequested();
            try
            {
                await sender.SendAsync(email, ct);
                MarkSuccess(email);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                MarkFailure(email, ex, maxRetryCount);
                failedEmails.Add(email);
            }
        }

        try
        {
            await repo.SaveChangesAsync(ct);
        }
        catch (Exception ex)
        {
            logger.LogCritical(
                ex,
                "Failed to persist batch email state for {Priority}. Potential inconsistencies!",
                priority);
        }

        logger.LogInformation(
            "Finished processing {Count} emails with {Priority} priority. Failed: {FailedCount}",
            emails.Count,
            priority,
            failedEmails.Count);
    }

    private async Task<List<Email>> AcquireEmailsAsync(
        IEmailRepository repo,
        int maxRetryCount,
        CancellationToken ct)
    {
        var emails = await repo.GetPendingEmailsAsync(maxRetryCount, priority, ct);

        foreach (var email in emails)
        {
            email.Status = EmailStatus.Sending;
        }

        await repo.SaveChangesAsync(ct);

        return emails;
    }

    private void MarkSuccess(Email email)
    {
        email.Status = EmailStatus.Sent;
        email.SentAt = DateTime.UtcNow;
        email.ErrorMessage = null;

        logger.LogInformation("Email {Id} sent successfully", email.Id);
    }

    private void MarkFailure(Email email, Exception ex, int maxRetryCount)
    {
        email.RetryCount++;
        email.ErrorMessage = ex.Message;

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

    private int GetMaxRetryCount() =>
        priority switch
        {
            EmailPriority.High => options.Value.HighPriorityRetryCount,
            EmailPriority.Normal => options.Value.MediumPriorityRetryCount,
            EmailPriority.Low => options.Value.LowPriorityRetryCount,
            _ => 3
        };
}