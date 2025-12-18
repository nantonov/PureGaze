using Common.Domain.Entities;

namespace Notification.Application.Contracts;

public interface IEmailSender
{
    Task<bool> SendAsync(Email email, CancellationToken cancellationToken = default);
}
