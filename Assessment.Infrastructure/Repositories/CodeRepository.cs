using Common.DAL;
using Microsoft.EntityFrameworkCore;

namespace Assessment.Application.Abstractions.Infrastructure;

public class CodeRepository(AppDbContext context) : ICodeRepository
{
    public async Task<int> GetCodeIdByProfessionalLevelIdAsync(Guid professionalLevelId, CancellationToken ct = default)
    => await context.Codes
        .AsNoTracking()
        .Where(c => c.GradeId == professionalLevelId)
        .Select(c => c.Id)
        .FirstOrDefaultAsync(ct);
}