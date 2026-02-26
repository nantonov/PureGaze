using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Evaluation.StartAssessmentStage;

public sealed record StartAssessmentStageCommand(int AssessmentStageId) : IRequest;