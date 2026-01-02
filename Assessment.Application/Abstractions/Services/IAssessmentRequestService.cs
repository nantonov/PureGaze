using Assessment.Application.Contracts.Application;

namespace Assessment.Application.Abstractions.Services;

public interface IAssessmentRequestService
{
    Task<int> AppointAsync(AppointAssessmentDto requestModel, CancellationToken cancellationToken);
}