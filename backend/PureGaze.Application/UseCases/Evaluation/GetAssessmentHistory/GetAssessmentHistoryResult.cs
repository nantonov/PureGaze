using PureGaze.Application.Contracts.Application;

namespace PureGaze.Application.UseCases.Evaluation.GetAssessmentHistory;

public sealed record GetAssessmentHistoryResult(int Total, IReadOnlyList<AssessmentHistoryItemDto> Items);
