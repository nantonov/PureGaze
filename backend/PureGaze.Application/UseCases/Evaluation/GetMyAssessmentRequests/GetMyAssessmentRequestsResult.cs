using PureGaze.Application.Contracts.Application;

namespace PureGaze.Application.UseCases.Evaluation.GetMyAssessmentRequests;

public sealed record GetMyAssessmentRequestsResult(IReadOnlyList<AssessmentRequestDto> AssessmentRequests);