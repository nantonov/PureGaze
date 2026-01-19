using PureGaze.Domain.Entities;

namespace PureGaze.Application.Abstractions.Infrastructure;

public interface ITemplateRepository
{
    Task<Template?> GetByCodeIdAsync(int codeId, CancellationToken ct = default);
}
