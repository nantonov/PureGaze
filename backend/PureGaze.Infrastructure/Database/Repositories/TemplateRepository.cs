using Microsoft.EntityFrameworkCore;
using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Domain.Entities;

namespace PureGaze.Infrastructure.Database.Repositories;

public class TemplateRepository(AppDbContext context) : ITemplateRepository
{
    public async Task AddAsync(Template template, CancellationToken ct = default) =>
        await context.Templates.AddAsync(template, ct);

    public async Task<Template?> GetByCodeIdAsync(int codeId, CancellationToken ct = default)
        => await context.Templates
            .Include(t => t.Topics)
            .FirstOrDefaultAsync(t => t.CodeId == codeId, ct);

    public async void Delete(Template template) =>
        context.Templates.Remove(template);

    public Task SaveChangesAsync(CancellationToken ct = default)
        => context.SaveChangesAsync(ct);
}