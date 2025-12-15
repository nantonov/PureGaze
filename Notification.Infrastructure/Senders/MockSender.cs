using Common.Domain.Entities;
using Notification.Application.Interfaces;

namespace Notification.Infrastructure.Senders;

public class MockSender : IEmailSender
{
    private readonly Random _random = new();

    public Task<bool> SendAsync(Email email, CancellationToken cancellationToken = default)
    {
        // Randomly succeed or fail
        bool success = _random.NextDouble() >= 0.3; // 70% success rate

        Console.WriteLine($"[MockSender] Attempting to send email to {email.To} with subject '{email.Subject}'...");
        
        if (success)
        {
            Console.WriteLine("[MockSender] Email sent successfully.");
        }
        else
        {
            Console.WriteLine("[MockSender] Failed to send email.");
        }

        return Task.FromResult(success);
    }
}
