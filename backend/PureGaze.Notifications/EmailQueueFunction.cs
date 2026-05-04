using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Domain.Enums;

namespace PureGaze.Notifications;

public class EmailQueueFunction(
    IEmailRepository repository,
    IEmailSender sender,
    ILogger<EmailQueueFunction> logger)
{
    [Function("EmailQueueFunction")]
    public async Task Run([QueueTrigger("email-queue")] string emailId, CancellationToken ct)
    {
        if (!Guid.TryParse(emailId, out var id))
        {
            logger.LogError("Invalid email id in queue: {EmailId}", emailId);
            return;
        }

        var email = await repository.GetByIdAsync(id, ct);

        if (email is null)
        {
            logger.LogWarning("Email {Id} not found in database", id);
            return;
        }

        email.Status = EmailStatus.Sending;
        await repository.SaveChangesAsync(ct);

        try
        {
            await sender.SendAsync(email, ct);

            email.Status = EmailStatus.Sent;
            email.SentAt = DateTime.UtcNow;

            logger.LogInformation("Email {Id} sent successfully", id);
        }
        catch (Exception ex)
        {
            email.RetryCount++;
            email.Status = email.RetryCount >= 3
                ? EmailStatus.ExceededRetryCount
                : EmailStatus.Failed;

            logger.LogError(ex, "Failed to send email {Id}. Retry {Retry}", id, email.RetryCount);
        }

        await repository.SaveChangesAsync(ct);
    }
}
