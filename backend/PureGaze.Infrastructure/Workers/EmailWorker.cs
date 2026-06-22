using PureGaze.Domain.Enums;
using PureGaze.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PureGaze.Application.Abstractions.Infrastructure;

namespace PureGaze.Infrastructure.Workers;

public sealed class EmailWorker(
    IServiceScopeFactory scopeFactory,
    IOptions<RetryPolicyOptions> options,
    ILogger<EmailWorker> logger)
    : BackgroundWorker(options.Value.DelayInSeconds)
{
    protected override async Task DoWorkAsync(CancellationToken ct)
    {
        using IServiceScope scope = scopeFactory.CreateScope();
        IEmailRepository repository = scope.ServiceProvider.GetRequiredService<IEmailRepository>();
        IEmailSender sender = scope.ServiceProvider.GetRequiredService<IEmailSender>();

        IReadOnlyList<Email> emails = await AcquireEmailsAsync(repository, ct);

        if (emails.Count == 0) return;

        foreach (Email email in emails)
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
                int maxRetryCount = options.Value.MaxRetryCount;

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

    private async Task<IReadOnlyList<Email>> AcquireEmailsAsync(
        IEmailRepository repository,
        CancellationToken ct)
    {
        IReadOnlyList<Email> emails =
            await repository.GetPendingEmailsAsync(ct);

        foreach (Email email in emails)
            email.Status = EmailStatus.Sending;

        await repository.SaveChangesAsync(ct);

        return emails;
    }
}
