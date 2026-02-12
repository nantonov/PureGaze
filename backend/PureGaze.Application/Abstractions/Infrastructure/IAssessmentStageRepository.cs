using PureGaze.Domain.Entities;

namespace PureGaze.Application.Abstractions.Infrastructure;

public interface IAssessmentStageRepository
{
    Task<AssessmentStage?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<AssessmentStage?> GetByIdWithScoresAndTopicAsync(int id, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}
