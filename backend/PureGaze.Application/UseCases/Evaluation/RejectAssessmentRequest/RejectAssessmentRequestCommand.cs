using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Evaluation.RejectAssessmentRequest;

public sealed record RejectAssessmentRequestCommand(int Id, string? Reason) : IRequest;