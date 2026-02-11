using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Abstractions.Providers;
using PureGaze.Application.Extensions;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Evaluation.GetAssignedToMeRequests;

public class GetAssignedToMeRequestsHandler(
    IAssessmentRequestRepository assessmentRequestRepository,
    ICurrentUserContextProvider currentUserContextProvider) 
    : IRequestHandler<GetAssignedToMeRequestsQuery, GetAssignedToMeRequestsResult>
{
    public async Task<GetAssignedToMeRequestsResult> Handle(GetAssignedToMeRequestsQuery query, CancellationToken ct)
    {
        var assessmentRequests = 
            await assessmentRequestRepository.GetByManagerEmailAsync(
                currentUserContextProvider.GetUserEmail(), 
                query.Page, 
                query.PageSize,
                ct);

        return new GetAssignedToMeRequestsResult([.. assessmentRequests.Select(x => x.ToDto())]);
    }
}