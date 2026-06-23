using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Evaluation.GetAssessmentRequestDetails;

public class GetAssessmentRequestDetailsHandler(
    IAssessmentRequestRepository assessmentRequestRepository)
    : IRequestHandler<GetAssessmentRequestDetailsQuery, GetAssessmentRequestDetailsResult>
{
    public async Task<GetAssessmentRequestDetailsResult> Handle(GetAssessmentRequestDetailsQuery query, CancellationToken ct)
    {
        AssessmentRequest assessmentRequest = await assessmentRequestRepository.GetByIdAsync(query.AssessmentRequestId, ct)
            ?? throw new KeyNotFoundException($"Assessment Request with Id {query.AssessmentRequestId} not found.");

        return new GetAssessmentRequestDetailsResult(GetAssessmentRequestDetailDto.ToDto(assessmentRequest));
    }
}
