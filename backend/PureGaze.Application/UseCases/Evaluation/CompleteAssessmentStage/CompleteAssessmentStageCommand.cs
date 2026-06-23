using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Evaluation.CompleteAssessmentStage;

public sealed record CompleteAssessmentStageCommand(
    int AssessmentStageId,
    bool IsRecommended,
    string? Summary = null) : IRequest;
