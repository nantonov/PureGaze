using PureGaze.Domain.Entities;

namespace PureGaze.Application.Abstractions.Infrastructure;

public interface IAssessmentRequestRepository
{
    Task<IReadOnlyList<AssessmentRequest>> GetByEmployeeIdAsync(int employeeId, int page, int pageSize, CancellationToken ct = default);
    Task<IReadOnlyList<AssessmentRequest>> GetByManagerIdAsync(int managerId, int page, int pageSize, CancellationToken ct = default);
    Task<AssessmentRequest?> GetByIdWithEmployeeAsync(int id, CancellationToken ct = default);
    Task<AssessmentRequest?> GetByIdAsync(int id, CancellationToken ct = default);
    Task AddAsync(AssessmentRequest assessmentRequest, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}