using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Abstractions.Providers;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Evaluation.GetMyAssessmentRequests;

public class GetMyAssessmentRequestsHandler(
    IAssessmentRequestRepository assessmentRequestRepository,
    ICurrentUserContextProvider currentUserContextProvider)
    : IRequestHandler<GetMyAssessmentRequestsQuery, GetMyAssessmentRequestsResult>
{
    public async Task<GetMyAssessmentRequestsResult> Handle(GetMyAssessmentRequestsQuery query, CancellationToken ct)
    {
        string email = currentUserContextProvider.GetUserEmail();

        IReadOnlyList<AssessmentRequest> assessmentRequests =
            await assessmentRequestRepository.GetByEmployeeEmailAsync(
                email, query.Page, query.PageSize, ct);

        int count = await assessmentRequestRepository.GetCountByEmployeeEmailAsync(email, ct);

        return new GetMyAssessmentRequestsResult(count,
            [.. assessmentRequests.Select(GetMyAssessmentRequestDto.ToDto)]);
    }
}
