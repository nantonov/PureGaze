using Common.Data.Enums;
using Common.Domain.Entities;

namespace Notification.Application.Abstractions.Infrastructure;

public interface IEmailRepository
{
    ValueTask<Email?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task AddAsync(Email email, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
    Task<List<Email>> GetPendingEmailsAsync(CancellationToken ct = default);
    Task<List<Email>> GetEmailsAsync(int page, int pageSize, EmailStatus? status, CancellationToken ct = default);
}