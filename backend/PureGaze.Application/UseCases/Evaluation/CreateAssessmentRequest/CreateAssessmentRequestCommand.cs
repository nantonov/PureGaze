using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Evaluation.CreateAssessmentRequest;

public sealed record CreateAssessmentRequestCommand(int EmployeeId) : IRequest;