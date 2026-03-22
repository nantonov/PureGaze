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
        var email = currentUserContextProvider.GetUserEmail();

        var assessmentRequests =
            await assessmentRequestRepository.GetByManagerEmailAsync(
                email, query.Page, query.PageSize, ct);

        var count = await assessmentRequestRepository.GetCountByManagerEmailAsync(email, ct);

        return new GetAssignedToMeRequestsResult(count,[.. assessmentRequests.Select(x => x.ToDto())]);
    }
}