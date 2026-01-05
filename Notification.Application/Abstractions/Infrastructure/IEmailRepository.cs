using Common.Data.Enums;
using Common.Domain.Entities;

namespace Notification.Application.Abstractions.Infrastructure;

public interface IEmailRepository
{
    Task AddAsync(Email email, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
    Task<List<Email>> GetPendingEmailsAsync(CancellationToken ct = default);
    Task<List<Email>> GetExceededEmailsAsync(CancellationToken ct = default);
    Task<List<Email>> GetEmailsAsync(int page, int pageSize, EmailStatus? status, CancellationToken ct = default);
}