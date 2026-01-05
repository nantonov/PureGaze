namespace Assessment.Application.Abstractions.Infrastructure;

public interface ICodeRepository
{
    Task<int> GetCodeIdByProfessionalLevelIdAsync(Guid professionalLevelId, CancellationToken ct = default);
}