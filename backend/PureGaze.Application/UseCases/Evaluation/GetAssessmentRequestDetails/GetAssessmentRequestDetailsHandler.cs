using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Extensions;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Evaluation.GetAssessmentRequestDetails;

public class GetAssessmentRequestDetailsHandler(
    IAssessmentRequestRepository assessmentRequestRepository)
    : IRequestHandler<GetAssessmentRequestDetailsQuery, GetAssessmentRequestDetailsResponse>
{
    public async Task<GetAssessmentRequestDetailsResponse> Handle(GetAssessmentRequestDetailsQuery query, CancellationToken ct)
    {
        var assessmentRequest = await assessmentRequestRepository.GetByIdAsync(query.AssessmentRequestId, ct)
            ?? throw new KeyNotFoundException($"Assessment Request with Id {query.AssessmentRequestId} not found.");

        return new GetAssessmentRequestDetailsResponse(assessmentRequest.ToDetailsDto());
    }
}