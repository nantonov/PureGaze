using PureGaze.Domain.Entities;

namespace PureGaze.Application.Abstractions.Infrastructure;

public interface IAssessmentRequestRepository
{
    Task<IReadOnlyList<AssessmentRequest>> GetByEmployeeEmailAsync(string employeeEmail, int page, int pageSize, CancellationToken ct = default);
    Task<IReadOnlyList<AssessmentRequest>> GetByManagerEmailAsync(string managerEmail, int page, int pageSize, CancellationToken ct = default);
    Task<AssessmentRequest?> GetEmployeeActiveAssessmentRequest(int employeeId, CancellationToken ct = default);
    Task<AssessmentRequest?> GetByIdWithEmployeeAsync(int id, CancellationToken ct = default);
    Task<AssessmentRequest?> GetByIdAsync(int id, CancellationToken ct = default);
    Task AddAsync(AssessmentRequest assessmentRequest, CancellationToken ct = default);
    Task<int> GetCountByManagerEmailAsync(string managerEmail, CancellationToken ct = default);
    Task<int> GetCountByEmployeeEmailAsync(string employeeEmail, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}