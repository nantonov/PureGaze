using Microsoft.EntityFrameworkCore;
using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Domain.Entities;

namespace PureGaze.Infrastructure.Database.Repositories;

public class TopicsRepository(AppDbContext context) : ITopicsRepository
{
    public async Task AddAsync(Topic topic, CancellationToken ct = default) =>
        await context.Topics.AddAsync(topic, ct);

    public async Task<Topic?> GetByIdAsync(int Id, CancellationToken ct = default)
        => await context.Topics
            .FirstOrDefaultAsync(t => t.Id == Id, ct);

    public async Task<Topic?> GetByTemplateAsync(int templateId, CancellationToken ct = default)
        => await context.Topics
            .FirstOrDefaultAsync(t => t.TemplateId == templateId, ct);

    public void Delete(Topic topic)
        => context.Topics.Remove(topic);

    public async Task SaveChangesAsync(CancellationToken ct = default)
        => await context.SaveChangesAsync(ct);
}