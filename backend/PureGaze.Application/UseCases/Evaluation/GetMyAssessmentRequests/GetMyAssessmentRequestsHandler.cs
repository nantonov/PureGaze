using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Abstractions.Providers;
using PureGaze.Application.Extensions;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Evaluation.GetMyAssessmentRequests;

public class GetMyAssessmentRequestsHandler(
    IAssessmentRequestRepository assessmentRequestRepository,
    ICurrentUserContextProvider currentUserContextProvider)
    : IRequestHandler<GetMyAssessmentRequestsQuery, GetMyAssessmentRequestsResult>
{
    public async Task<GetMyAssessmentRequestsResult> Handle(GetMyAssessmentRequestsQuery requests, CancellationToken ct)
    {
        var assessmentRequests =
            await assessmentRequestRepository.GetByEmployeeEmailAsync(
                currentUserContextProvider.GetUserEmail(),
                requests.Page,
                requests.PageSize,
                ct);

        return new GetMyAssessmentRequestsResult([.. assessmentRequests.Select(x => x.ToDto())]);
    }
}