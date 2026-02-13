using PureGaze.Domain.Entities;

namespace PureGaze.Application.Abstractions.Infrastructure;

public interface ITemplateRepository
{
    Task<Template?> GetByCodeIdAsync(int codeId, CancellationToken ct = default);
    Task AddAsync(Template template, CancellationToken ct = default);
    void Remove(Template template);
    Task SaveChangesAsync(CancellationToken ct = default);
}
