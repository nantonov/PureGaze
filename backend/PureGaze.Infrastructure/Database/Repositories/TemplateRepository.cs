using Microsoft.EntityFrameworkCore;
using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Domain.Entities;

namespace PureGaze.Infrastructure.Database.Repositories;

public class TemplateRepository(AppDbContext context) : ITemplateRepository
{
    public async Task AddAsync(Template template, CancellationToken ct = default) =>
        await context.Templates.AddAsync(template, ct);

    public async Task<Template?> GetByIdAsync(int Id, CancellationToken ct = default)
        => await context.Templates
            .Include(t => t.Topics)
            .FirstOrDefaultAsync(t => t.Id == Id, ct);

    public async Task<Template?> GetByCodeIdAsync(int codeId, CancellationToken ct = default)
        => await context.Templates
            .Include(t => t.Topics)
            .FirstOrDefaultAsync(t => t.CodeId == codeId, ct);

    public void Delete(Template template)
        => context.Templates.Remove(template);

    public async Task SaveChangesAsync(CancellationToken ct = default)
        => await context.SaveChangesAsync(ct);

    public IAsyncEnumerable<Template> Query(int page, int pageSize)
        => context.Templates
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .AsAsyncEnumerable();
}