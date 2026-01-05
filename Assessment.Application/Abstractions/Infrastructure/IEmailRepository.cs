using Common.Domain.Entities;

namespace Notification.Application.Abstractions.Infrastructure;

public interface IEmailRepository
{
    Task AddAsync(Email email, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}