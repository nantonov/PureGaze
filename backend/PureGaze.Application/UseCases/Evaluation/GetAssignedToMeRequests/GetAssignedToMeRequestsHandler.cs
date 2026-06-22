using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Abstractions.Providers;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Evaluation.GetAssignedToMeRequests;

public class GetAssignedToMeRequestsHandler(
    IAssessmentRequestRepository assessmentRequestRepository,
    ICurrentUserContextProvider currentUserContextProvider)
    : IRequestHandler<GetAssignedToMeRequestsQuery, GetAssignedToMeRequestsResult>
{
    public async Task<GetAssignedToMeRequestsResult> Handle(GetAssignedToMeRequestsQuery query, CancellationToken ct)
    {
        string email = currentUserContextProvider.GetUserEmail();

        IReadOnlyList<AssessmentRequest> assessmentRequests =
            await assessmentRequestRepository.GetByManagerEmailAsync(
                email, query.Page, query.PageSize, ct);

        int count = await assessmentRequestRepository.GetCountByManagerEmailAsync(email, ct);

        return new GetAssignedToMeRequestsResult(count,
            [.. assessmentRequests.Select(GetAssignedToMeRequestDto.ToDto)]);
    }
}
