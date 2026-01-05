using Common.Domain.Entities;

namespace Assessment.Application.Abstractions.Infrastructure;

public interface IAssessmentRequestRepository
{
    Task AddAsync(AssessmentRequest assessmentRequest, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}