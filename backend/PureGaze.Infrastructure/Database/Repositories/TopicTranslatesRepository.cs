using Microsoft.EntityFrameworkCore;
using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Domain.Entities;
using PureGaze.Domain.Enums;

namespace PureGaze.Infrastructure.Database.Repositories;

public class TopicTranslatesRepository(AppDbContext context) : ITopicTranslatesRepository
{
    public async Task<IReadOnlyList<TopicTranslate>> GetTopicsTranslatesAsync(int topicId, int page, int pageSize, CancellationToken ct = default)
        => await context.TopicTranslates
            .Where(x => x.TopicId == topicId)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync(ct);

    public async Task<TopicTranslate?> GetByTopicIdAndLanguageAsync(int TopicId, Language language, CancellationToken ct = default)
        => await context.TopicTranslates
            .FirstOrDefaultAsync(t => t.TopicId == TopicId && t.Language == language, ct);

    public async Task AddAsync(TopicTranslate topicTranslate, CancellationToken ct = default) =>
        await context.TopicTranslates.AddAsync(topicTranslate, ct);

    public void Delete(TopicTranslate topicTranslate)
        => context.TopicTranslates.Remove(topicTranslate);

    public async Task SaveChangesAsync(CancellationToken ct = default)
        => await context.SaveChangesAsync(ct);
}