using Microsoft.EntityFrameworkCore;
using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Domain.Entities;

namespace PureGaze.Infrastructure.Database.Repositories;

public class SubtopicScoreRepository(AppDbContext context)
    : ISubtopicScoreRepository
{
    public async Task AddAsync(SubtopicScore subtopic, CancellationToken ct = default)
        => await context.SubtopicScores.AddAsync(subtopic, ct);

    public async Task<SubtopicScore?> GetBySubtopicAndStageIdAsync(int subtopicId, int stageId, CancellationToken ct = default)
        => await context.SubtopicScores
            .FirstOrDefaultAsync(s => s.SubtopicId == subtopicId && s.StageId == stageId, ct);

    public async Task SaveChangesAsync(CancellationToken ct = default)
        => await context.SaveChangesAsync(ct);
}
