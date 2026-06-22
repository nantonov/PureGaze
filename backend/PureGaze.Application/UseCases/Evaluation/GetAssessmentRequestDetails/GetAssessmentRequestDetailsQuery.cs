using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Evaluation.GetAssessmentRequestDetails;

public sealed record GetAssessmentRequestDetailsQuery(int AssessmentRequestId)
    : IRequest<GetAssessmentRequestDetailsResult>;