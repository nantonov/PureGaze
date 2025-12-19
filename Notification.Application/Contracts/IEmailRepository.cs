using Common.Data.Enums;
using Common.Domain.Entities;

namespace Notification.Application.Contracts;

public interface IEmailRepository
{
    Task AddAsync(Email email, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
    Task<List<Email>> GetFailedEmailsReadOnlyAsync(EmailPriority? priority = null, CancellationToken ct = default);
    Task<List<Email>> GetPendingEmailsAsync(int maxRetryCount, EmailPriority priority, CancellationToken ct = default);
    
}
