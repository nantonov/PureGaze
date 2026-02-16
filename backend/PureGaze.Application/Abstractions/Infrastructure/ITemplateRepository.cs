using PureGaze.Domain.Entities;

namespace PureGaze.Application.Abstractions.Infrastructure;

public interface ITemplateRepository
{
    Task<Template?> GetByCodeIdAsync(int codeId, CancellationToken ct = default);
    Task<Template?> GetByIdAsync(int id, CancellationToken ct = default);
    IAsyncEnumerable<Template> Query(int page, int pageSize);
    Task AddAsync(Template template, CancellationToken ct = default);
    void Delete(Template template);
    Task SaveChangesAsync(CancellationToken ct = default);
}
