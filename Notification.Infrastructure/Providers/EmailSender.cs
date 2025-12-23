using Common.Domain.Entities;
using Notification.Application.Abstractions.Infrastructure;

namespace Notification.Infrastructure.Providers;

public class EmailSender : IEmailSender
{
    public async Task<bool> SendAsync(Email email, CancellationToken cancellationToken = default)
    {
        return true;
    }
}