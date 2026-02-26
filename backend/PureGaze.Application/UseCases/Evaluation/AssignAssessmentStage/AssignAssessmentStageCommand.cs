using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Evaluation.AssignAssessmentStage;

public sealed record AssignAssessmentStageCommand(int AssessmentStageId)
    : IRequest;