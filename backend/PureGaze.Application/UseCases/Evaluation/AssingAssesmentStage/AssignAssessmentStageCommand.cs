using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Evaluation.AssingAssesmentStage;

public sealed record AssignAssessmentStageCommand(int AssessmentStageId, string ManagerEmail) 
    : IRequest;