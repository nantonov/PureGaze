using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Evaluation.RejectAssessmentRequest;

public sealed record RejectAssessmentRequestCommand(int RequestId, string? Reason) : IRequest;
