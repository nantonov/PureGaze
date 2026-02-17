using Microsoft.EntityFrameworkCore;
using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Domain.Entities;

namespace PureGaze.Infrastructure.Database.Repositories;

public class TopicsRepository(AppDbContext context) : ITopicsRepository
{
    public async Task<IReadOnlyList<Topic>> GetTopicsByTemplateIdAsync(int templateId, int page, int pageSize, CancellationToken ct = default)
        => await context.Topics
            .Include(x=> x.TopicTranslates)
            .Where(x => x.TemplateId == templateId)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync(ct);
    
    public async Task AddAsync(Topic topic, CancellationToken ct = default) =>
        await context.Topics.AddAsync(topic, ct);

    public async Task<Topic?> GetByIdAsync(int id, CancellationToken ct = default)
        => await context.Topics
            .Include(x => x.TopicTranslates)
            .FirstOrDefaultAsync(t => t.Id == id, ct);

    public void Delete(Topic topic) => context.Topics.Remove(topic);

    public async Task SaveChangesAsync(CancellationToken ct = default)
        => await context.SaveChangesAsync(ct);
}