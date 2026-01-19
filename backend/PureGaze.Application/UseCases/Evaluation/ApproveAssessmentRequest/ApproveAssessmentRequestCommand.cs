using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Evaluation.ApproveAssessmentRequest;

public record ApproveAssessmentRequestCommand(int Id) : IRequest;
