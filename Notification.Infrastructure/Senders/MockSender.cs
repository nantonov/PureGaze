using Common.Domain.Entities;
using Microsoft.Extensions.Logging;
using Notification.Application.Interfaces;

namespace Notification.Infrastructure.Senders;

public class MockSender(ILogger<MockSender> logger) : IEmailSender
{
    private readonly Random _random = new();

    public Task<bool> SendAsync(Email email, CancellationToken cancellationToken = default)
    {
        // Randomly succeed or fail
        bool success = _random.NextDouble() >= 0.3; // 70% success rate

        logger.LogInformation(
            "[MockSender] Attempting to send email to {To} with subject '{Subject}'...",
            email.To,
            email.Subject
        );

        if (success)
        {
            logger.LogInformation("[MockSender] Email sent successfully.");
        }
        else
        {
            logger.LogWarning("[MockSender] Failed to send email.");
        }

        return Task.FromResult(success);
    }
}
