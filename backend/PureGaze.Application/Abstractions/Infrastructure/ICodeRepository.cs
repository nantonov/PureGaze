using PureGaze.Domain.Entities;

namespace PureGaze.Application.Abstractions.Infrastructure;

public interface ICodeRepository
{
    Task<Code?> GetByProfessionalLevelIdAsync(Guid professionalLevelId, CancellationToken ct = default);
    Task <Code?> GetByIdAsync(int codeId, CancellationToken ct = default);
    Task<IReadOnlyList<Code>> GetAllAsync(CancellationToken ct = default);
    Task AddAsync(Code code, CancellationToken ct = default);
    void Delete(Code code);
    Task SaveChangesAsync(CancellationToken ct = default);
}