using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Extensions;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Evaluation.GetMyAssessmentRequests;

public class GetMyAssessmentRequestsHandler(
    IAssessmentRequestRepository assessmentRequestRepository)
    : IRequestHandler<GetMyAssessmentRequestsQuery, GetMyAssessmentRequestsResult>
{
    public async Task<GetMyAssessmentRequestsResult> Handle(GetMyAssessmentRequestsQuery requests, CancellationToken ct)
    {
        var assessmentRequests = 
            await assessmentRequestRepository.GetByEmployeeIdAsync(requests.EmployeeId, requests.Page, requests.PageSize, ct);
        
        return new GetMyAssessmentRequestsResult
        {
            AssessmentRequests = [.. assessmentRequests.Select(x => x.ToDto())]
        };
    }
}