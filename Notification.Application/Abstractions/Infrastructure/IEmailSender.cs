using Common.Domain.Entities;

namespace Notification.Application.Abstractions.Infrastructure;

public interface IEmailSender
{
    Task SendAsync(Email email, CancellationToken ct = default);
}