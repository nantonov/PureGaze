using PureGaze.Domain.Entities;

namespace PureGaze.Application.Abstractions.Infrastructure;

public interface ISubtopicScoreRepository
{
    Task<SubtopicScore?> GetBySubtopicAndStageIdAsync(int subtopicId, int stageId, CancellationToken ct = default);
    Task AddAsync(SubtopicScore subtopicScore, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}