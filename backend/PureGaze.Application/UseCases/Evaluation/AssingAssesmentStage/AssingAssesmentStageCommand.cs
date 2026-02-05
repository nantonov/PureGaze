using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Evaluation.AssingAssesmentStage;

public sealed record AssingAssesmentStageCommand(int AssessmentStageId, string ManagerEmail) : IRequest;