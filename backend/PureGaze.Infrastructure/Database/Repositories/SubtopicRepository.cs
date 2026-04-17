using PureGaze.Domain.Entities;
using PureGaze.Application.Abstractions.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace PureGaze.Infrastructure.Database.Repositories;

public class SubtopicRepository(AppDbContext context)
    : ISubtopicRepository
{
    public async Task<Subtopic?> GetByIdAsync(int id, CancellationToken ct = default)
        => await context.Subtopics
            .Include(s => s.SubtopicTranslates)
            .FirstOrDefaultAsync(s => s.Id == id, ct);

    public async Task<IReadOnlyList<Subtopic>> GetByTopicIdAsync(int topicId, CancellationToken ct = default)
        => await context.Subtopics
            .Include(s => s.SubtopicTranslates)
            .Where(s => s.TopicId == topicId)
            .AsNoTracking()
            .ToListAsync(ct);

    public async Task<bool> IsNameExistingAsync(int topicId, IEnumerable<string> names,
        int? excludeSubtopicId = null, CancellationToken ct = default)
        => await context.Subtopics
            .Where(s => s.TopicId == topicId &&
                        (!excludeSubtopicId.HasValue || s.Id != excludeSubtopicId.Value))
            .SelectMany(s => s.SubtopicTranslates)
            .AnyAsync(st => names.Contains(st.Name), cancellationToken: ct);

    public async Task AddAsync(Subtopic subtopic, CancellationToken ct = default)
        => await context.Subtopics.AddAsync(subtopic, ct);

    public void Delete(Subtopic subtopic)
        => context.Subtopics.Remove(subtopic);

    public async Task SaveChangesAsync(CancellationToken ct = default)
        => await context.SaveChangesAsync(ct);
}
