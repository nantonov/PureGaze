using PureGaze.Application.Contracts.Application;

namespace PureGaze.Application.UseCases.Evaluation.GetAssignedToMeRequests;

public sealed record GetAssignedToMeRequestsResult(int Total, IReadOnlyList<AssessmentRequestDto> AssessmentRequests);