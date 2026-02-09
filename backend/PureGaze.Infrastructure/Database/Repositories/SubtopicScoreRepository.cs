using PureGaze.Domain.Entities;
using PureGaze.Application.Abstractions.Infrastructure;

namespace PureGaze.Infrastructure.Database.Repositories;

public class SubtopicScoreRepository(AppDbContext context) 
    : ISubtopicScoreRepository
{
    public async Task AddAsync(SubtopicScore subtopic, CancellationToken ct = default)
        => await context.SubtopicScores.AddAsync(subtopic, ct);

    public async Task SaveChangesAsync(CancellationToken ct = default)
        => await context.SaveChangesAsync(ct);
}
