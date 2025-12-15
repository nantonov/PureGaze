using Common.Domain.Entities;

namespace Notification.Application.Interfaces;

public interface IEmailRepository
{
    Task AddAsync(Email email, CancellationToken cancellationToken = default);
    Task UpdateAsync(Email email, CancellationToken cancellationToken = default);
}
