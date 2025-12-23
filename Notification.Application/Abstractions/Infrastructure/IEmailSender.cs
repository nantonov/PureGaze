using Common.Domain.Entities;

namespace Notification.Application.Abstractions.Infrastructure;

public interface IEmailSender
{
    Task<bool> SendAsync(Email email, CancellationToken cancellationToken = default);
}