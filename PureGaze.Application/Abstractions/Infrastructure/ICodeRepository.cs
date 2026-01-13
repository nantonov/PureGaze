using PureGaze.Domain.Entities;

namespace PureGaze.Application.Abstractions.Infrastructure;

public interface ICodeRepository
{
    Task<Code?> GetByProfessionalLevelIdAsync(Guid professionalLevelId, CancellationToken ct = default);
}