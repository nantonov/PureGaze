using Microsoft.EntityFrameworkCore;
using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Domain.Entities;

namespace PureGaze.Infrastructure.Database.Repositories;

public class TopicTranslatesRepository(AppDbContext context) : ITopicTranslatesRepository
{
    public async Task<IReadOnlyList<TopicTranslate>> GetTopicTranslatesAsync(int topicId, CancellationToken ct = default)
        => await context.TopicTranslates
            .Where(x => x.TopicId == topicId)
            .ToListAsync(ct);

    public async Task AddAsync(TopicTranslate topicTranslate, CancellationToken ct = default) =>
        await context.TopicTranslates.AddAsync(topicTranslate, ct);

    public async Task DeleteTranslatesForTopicAsync(int topicId, CancellationToken ct = default)
        => await context.TopicTranslates
            .Where(x => x.TopicId == topicId)
            .ExecuteDeleteAsync();

    public async Task SaveChangesAsync(CancellationToken ct = default)
        => await context.SaveChangesAsync(ct);
}