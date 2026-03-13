using PureGaze.Application.Contracts.Application;

namespace PureGaze.Application.UseCases.Evaluation.GetMyAssessmentRequests;

public sealed record GetMyAssessmentRequestsResult(int Total, IReadOnlyList<AssessmentRequestDto> AssessmentRequests);