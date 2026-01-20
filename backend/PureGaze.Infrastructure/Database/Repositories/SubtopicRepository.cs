using PureGaze.Domain.Entities;
using PureGaze.Application.Abstractions.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace PureGaze.Infrastructure.Database.Repositories;

public class SubtopicRepository(AppDbContext context) : ISubtopicRepository
{
    public async Task<Subtopic?> GetByIdAsync(int id, CancellationToken ct = default)
        => await context.Subtopics
            .Include(s => s.SubtopicTranslates)
            .FirstOrDefaultAsync(s => s.Id == id, ct);

    public async Task<string?> GetAnyExistingNameAsync(int topicId, IEnumerable<string> names, int? excludeSubtopicId = null, CancellationToken ct = default)
        => await context.Subtopics
            .Where(s => s.TopicId == topicId && 
                        (!excludeSubtopicId.HasValue || s.Id != excludeSubtopicId.Value))
            .SelectMany(s => s.SubtopicTranslates)
            .Where(st => names.Contains(st.Name))
            .Select(st => st.Name)
            .FirstOrDefaultAsync(ct);

    public async Task AddAsync(Subtopic subtopic, CancellationToken ct = default)
        => await context.Subtopics.AddAsync(subtopic, ct);

    public async Task DeleteAsync(Subtopic subtopic, CancellationToken ct = default)
    {
        context.Subtopics.Remove(subtopic);
        await Task.CompletedTask;
    }

    public async Task SaveChangesAsync(CancellationToken ct = default)
        => await context.SaveChangesAsync(ct);
}
