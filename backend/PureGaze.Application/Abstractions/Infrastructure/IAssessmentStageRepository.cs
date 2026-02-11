using PureGaze.Domain.Entities;

namespace PureGaze.Application.Abstractions.Infrastructure;

public interface IAssessmentStageRepository
{
    Task<AssessmentStage?> GetByIdAsync(int id, CancellationToken ct = default);
    void Update(AssessmentStage assessmentStage);
    Task SaveChangesAsync(CancellationToken ct = default);
}
