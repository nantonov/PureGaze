using Common.Domain.Entities;

namespace Notification.Application.Interfaces;

public interface IEmailSender
{
    Task<bool> SendAsync(Email email, CancellationToken cancellationToken = default);
}
