using Microsoft.EntityFrameworkCore;
using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Domain.Entities;

namespace PureGaze.Infrastructure.Database.Repositories;

public class AssessmentStageRepository(AppDbContext context)
    : IAssessmentStageRepository
{
    public async Task<AssessmentStage?> GetByIdAsync(int id, CancellationToken ct = default)
        => await context.AssessmentStages
            .Include(x => x.Assessment)
            .Include(x => x.Assessor)
            .FirstOrDefaultAsync(x => x.Id == id, ct);

    public async Task<AssessmentStage?> GetByIdWithScoresAndTopicAsync(int id, CancellationToken ct = default)
        => await context.AssessmentStages
            .Include(x => x.Assessment)
            .Include(x => x.Assessor)
            .Include(x => x.Topic).ThenInclude(x => x.Subtopics)
            .Include(x => x.Scores)
            .FirstOrDefaultAsync(x => x.Id == id, ct);

    public async Task SaveChangesAsync(CancellationToken ct = default)
        => await context.SaveChangesAsync(ct);
}
