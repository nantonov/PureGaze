using Microsoft.EntityFrameworkCore;
using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Domain.Entities;

namespace PureGaze.Infrastructure.Database.Repositories;

public class AssessmentRepository(AppDbContext context) : IAssessmentRepository
{
    public async Task AddAsync(Assessment assessment, CancellationToken ct = default)
    {
        await context.Assessments.AddAsync(assessment, ct);
    }

    public async Task<Assessment?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await context.Assessments
            .Include(x => x.Stages)
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }
}
