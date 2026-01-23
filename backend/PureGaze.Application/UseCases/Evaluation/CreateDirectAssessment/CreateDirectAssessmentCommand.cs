using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Evaluation.CreateDirectAssessment;

public sealed record CreateDirectAssessmentCommand(int ManagerId, int EmployeeId) : IRequest;
