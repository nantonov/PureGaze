using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Evaluation.UnassignAssessmentStage;

public sealed record UnassignAssessmentStageCommand(int AssessmentStageId) : IRequest;
