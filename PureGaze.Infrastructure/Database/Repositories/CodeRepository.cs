using Microsoft.EntityFrameworkCore;
using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Domain.Entities;

namespace PureGaze.Infrastructure.Database.Repositories;

public class CodeRepository(AppDbContext context) 
    : ICodeRepository
{
    public async Task<Code?> GetByProfessionalLevelIdAsync(Guid professionalLevelId, CancellationToken ct = default)
        => await context.Codes.FirstOrDefaultAsync(c => c.GradeId == professionalLevelId, ct);
}