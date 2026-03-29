using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Admin.Codes.CreateCode;

public sealed record CreateCodeCommand(
    Guid GradeId,
    Guid ToGradeId,
    string? Name,
    int TotalEx,
    int DiffEx) : IRequest;