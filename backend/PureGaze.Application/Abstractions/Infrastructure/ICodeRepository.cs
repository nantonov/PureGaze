using PureGaze.Domain.Entities;

namespace PureGaze.Application.Abstractions.Infrastructure;

public interface ICodeRepository
{
    Task<Code?> GetByProfessionalLevelIdAsync(Guid professionalLevelId, CancellationToken ct = default);
    Task <Code?> GetByIdAsync(int codeId, CancellationToken ct = default);
    Task<(List<Code> items, int totalCount)> GetAllAsync(CancellationToken ct = default, int? skip = null,
        int? take = null);
    Task<Code?> DeleteAsync(Code code, CancellationToken ct = default);
    Task<Code> AddAsync(Code code, CancellationToken ct = default);
    Task<Code?> UpdateAsync(Code updated, CancellationToken ct = default);
}