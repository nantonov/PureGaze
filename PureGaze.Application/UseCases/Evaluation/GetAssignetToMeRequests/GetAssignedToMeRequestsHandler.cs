using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Extensions;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Evaluation.GetAssignetToMeRequests;

public class GetAssignedToMeRequestsHandler(
    IAssessmentRequestRepository assessmentRequestRepository) 
    : IRequestHandler<GetAssignedToMeRequestsQuery, GetAssignedToMeRequestsResult>
{
    public async Task<GetAssignedToMeRequestsResult> Handle(GetAssignedToMeRequestsQuery query, CancellationToken ct)
    {
        var assessmentRequests = 
            await assessmentRequestRepository.GetByManagerIdAsync(query.ManagerId, query.Page, query.PageSize, ct);

        return new GetAssignedToMeRequestsResult
        { 
            AssessmentRequests = [.. assessmentRequests.Select(x => x.ToDto())]
        };
    }
}