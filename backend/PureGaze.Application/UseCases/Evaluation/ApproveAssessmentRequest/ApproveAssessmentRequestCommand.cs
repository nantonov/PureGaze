using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Evaluation.ApproveAssessmentRequest;

public sealed record ApproveAssessmentRequestCommand(int Id) : IRequest;
