using Assessment.Application.Abstractions.Infrastructure;
using Common.DAL;
using Common.Domain.Entities;

namespace Assessment.Infrastructure.Repositories;

public class AssessmentRequestRepository(AppDbContext context) : IAssessmentRequestRepository
{
    public async Task AddAsync(AssessmentRequest assessment, CancellationToken ct = default) 
        => await context.AssessmentRequests.AddAsync(assessment, ct);

    public async Task SaveChangesAsync(CancellationToken ct = default)
        => await context.SaveChangesAsync(ct);
}
