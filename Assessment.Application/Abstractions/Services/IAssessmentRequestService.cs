using Assessment.Application.Contracts.Application;

namespace Assessment.Application.Abstractions.Services;

public interface IAssessmentRequestService
{
    Task<int> AppointAsync(AppointAssessmentRequest request, CancellationToken ct);
    Task<AssessmentRequestDetailsDto> GetDetailsAsync(int assessmentRequestId, CancellationToken ct);
    Task<IReadOnlyList<AssessmentRequestDetailsDto>> GetMyAssessmentsAsync(int employeeId, int page, int pageSize, CancellationToken ct);
    Task<IReadOnlyList<AssessmentRequestDetailsDto>> GetAssignedAssessmentsAsync(int managerId, int page, int pageSize, CancellationToken ct);
}