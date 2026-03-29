using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Admin.Codes.EditCode;

public sealed record EditCodeCommand(
    int Id,
    Guid GradeId,
    Guid ToGradeId,
    string? Name,
    int TotalEx,
    int DiffEx,
    string? LevelVisionRu,
    string? LevelVisionEn) : IRequest;