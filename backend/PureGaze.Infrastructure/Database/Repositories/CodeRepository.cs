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

    public async Task<IReadOnlyList<Code>> GetAllAsync(CancellationToken ct = default)
        => await context.Codes
            .Include(c => c.CodeTranslates)
            .AsNoTracking()
            .ToListAsync(ct);

    public async Task AddAsync(Code code, CancellationToken ct = default)
        => await context.AddAsync(code, ct);

    public void Delete(Code code)
        => context.Codes.Remove(code);

    public async Task SaveChangesAsync(CancellationToken ct = default)
        => await context.SaveChangesAsync(ct);
}