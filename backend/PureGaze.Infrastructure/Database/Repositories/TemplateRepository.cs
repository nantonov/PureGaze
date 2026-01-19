using Microsoft.EntityFrameworkCore;
using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Domain.Entities;

namespace PureGaze.Infrastructure.Database.Repositories;

public class TemplateRepository(AppDbContext context) : ITemplateRepository
{
    public async Task<Template?> GetByCodeIdAsync(int codeId, CancellationToken ct = default)
    {
        return await context.Templates
            .Include(t => t.Topics)
            .FirstOrDefaultAsync(t => t.CodeId == codeId, ct);
    }
}
