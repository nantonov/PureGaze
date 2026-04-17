using Microsoft.EntityFrameworkCore;
using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Domain.Entities;

namespace PureGaze.Infrastructure.Database.Repositories;

public class TemplateRepository(AppDbContext context) : ITemplateRepository
{
    public async Task<IReadOnlyList<Template>> GetTemplates(int page, int pageSize, CancellationToken ct = default)
        => await context.Templates
            .Include(t => t.Code)
                .ThenInclude(c => c!.Grade)
            .Include(t => t.Code)
                .ThenInclude(c => c!.ToGrade)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync(ct);

    public async Task<Template?> GetByIdAsync(int id, CancellationToken ct = default)
        => await context.Templates
            .Include(t => t.Topics)
            .FirstOrDefaultAsync(t => t.Id == id, ct);

    public async Task<Template?> GetByCodeIdAsync(int codeId, CancellationToken ct = default)
        => await context.Templates
            .Include(t => t.Topics)
            .FirstOrDefaultAsync(t => t.CodeId == codeId, ct);

    public async Task AddAsync(Template template, CancellationToken ct = default) =>
        await context.Templates.AddAsync(template, ct);

    public void Delete(Template template)
        => context.Templates.Remove(template);

    public async Task SaveChangesAsync(CancellationToken ct = default)
        => await context.SaveChangesAsync(ct);
}