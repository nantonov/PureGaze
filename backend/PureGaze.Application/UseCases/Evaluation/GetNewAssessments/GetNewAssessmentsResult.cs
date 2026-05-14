using PureGaze.Application.Contracts.Application;

namespace PureGaze.Application.UseCases.Evaluation.GetNewAssessments;

public sealed record GetNewAssessmentsResult(IReadOnlyList<AssessmentListItemDto> Items);
