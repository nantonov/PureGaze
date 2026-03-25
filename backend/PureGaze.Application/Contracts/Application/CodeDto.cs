namespace PureGaze.Application.Contracts.Application;

public sealed record CodeDto(
    int Id,
    Guid GradeId,
    Guid ToGradeId,
    string? Display,
    int TotalEx,
    int DiffEx,
    string? LevelVisionRu,
    string? LevelVisionEn);