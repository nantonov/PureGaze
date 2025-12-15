using Common.Domain.Entities;
using Notification.Application.Interfaces;

namespace Notification.Application.Strategies;

public interface INotificationStrategy
{
    Task ProcessAsync(Email email, IEmailRepository repository, IEmailSender sender, CancellationToken cancellationToken = default);
}
