using Microsoft.EntityFrameworkCore;
using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Domain.Entities;

namespace PureGaze.Infrastructure.Database.Repositories;

public class SubtopicScoreRepository(AppDbContext context) 
    : ISubtopicScoreRepository
{
    public async Task AddAsync(SubtopicScore subtopic, CancellationToken ct = default)
        => await context.SubtopicScores.AddAsync(subtopic, ct);

    public async Task<SubtopicScore?> GetBySubtopicAndStageIdAsync(int SubtopicId, int StageId, CancellationToken ct = default)
        => await context.SubtopicScores
            .FirstOrDefaultAsync(s => s.SubtopicId == SubtopicId && s.StageId == StageId, ct);

    public async Task SaveChangesAsync(CancellationToken ct = default)
        => await context.SaveChangesAsync(ct);

    public async Task UpdateAsync(SubtopicScore subtopicScore, CancellationToken ct = default) 
        => context.Update(subtopicScore);
}
