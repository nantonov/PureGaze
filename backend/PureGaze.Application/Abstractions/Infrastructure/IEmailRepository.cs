using PureGaze.Domain.Entities;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.Abstractions.Infrastructure;

public interface IEmailRepository
{
    ValueTask<Email?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IList<Email>> GetPendingEmailsAsync(CancellationToken ct = default);
    Task<IList<Email>> GetEmailsAsync(int page, int pageSize, EmailStatus? status, CancellationToken ct = default);
    Task AddAsync(Email email, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}