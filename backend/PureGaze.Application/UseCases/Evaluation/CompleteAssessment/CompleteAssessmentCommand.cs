using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Evaluation.CompleteAssessment;

public sealed record CompleteAssessmentCommand(int AssessmentId) : IRequest;
