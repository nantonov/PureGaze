using Microsoft.EntityFrameworkCore;
using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Domain.Entities;

namespace PureGaze.Infrastructure.Database.Repositories;

public class CodeRepository(AppDbContext context)
    : ICodeRepository
{
    public async Task<Code?> GetByProfessionalLevelIdAsync(Guid professionalLevelId, CancellationToken ct = default)
        => await context
            .Codes
            .FirstOrDefaultAsync(c => c.GradeId == professionalLevelId, ct);

    public async Task<Code?> GetByIdAsync(int codeId, CancellationToken ct = default)
        => await context
            .Codes
            .Include(r => r.CodeTranslates)
            .FirstOrDefaultAsync(c => c.Id == codeId, ct);

    public async Task<(List<Code> items, int totalCount)> GetAllAsync(CancellationToken ct = default, int? skip = null,
        int? take = null)
    {
        IQueryable<Code> query = context.Codes.AsQueryable();
        var totalCount = await query.CountAsync(ct);

        query = query
            .Include(r => r.CodeTranslates);

        if (skip.HasValue) query = query.Skip(skip.Value);
        if (take.HasValue) query = query.Take(take.Value);

        var items = await query.ToListAsync(ct);
        return (items, totalCount);
    }

    public async Task<Code?> DeleteAsync(Code code, CancellationToken ct = default)
    {
        var existing = await context.FindAsync<Code>([code.Id], cancellationToken: ct);
        if (existing == null) return null;
        context.Codes.Remove(code);
        await context.SaveChangesAsync(ct);
        return code;
    }

    public async Task<Code> AddAsync(Code code, CancellationToken ct = default)
    {
        await context.AddAsync(code, ct);
        await context.SaveChangesAsync(ct);
        return code;
    }

    public async Task<Code?> UpdateAsync(Code updated, CancellationToken ct = default)
    {
        var existing = await context.FindAsync<Code>([updated.Id], cancellationToken: ct);
        if (existing == null) return null;
        context.Entry(existing).CurrentValues.SetValues(updated);
        existing.UpdatedAt = DateTime.UtcNow;
        await context.SaveChangesAsync(ct);
        return existing;
    }
}