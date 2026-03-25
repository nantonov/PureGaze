using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Admin.Codes.CreateCode;

public sealed record CreateCodeCommand(
    Guid GradeId,
    Guid ToGradeId,
    string? Display,
    int TotalEx,
    int DiffEx,
    string? LevelVisionRu,
    string? LevelVisionEn) : IRequest;