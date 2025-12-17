using Common.Domain.Entities;
using Common.Domain.Enums;

namespace Notification.Application.Interfaces;

public interface IEmailRepository
{
    Task AddAsync(Email email, CancellationToken cancellationToken = default);
    Task UpdateAsync(Email email, CancellationToken cancellationToken = default);
    Task<List<Email>> GetFailedEmailsAsync(int retryCount, EmailPriority priority, CancellationToken cancellationToken = default);
}
