namespace PureGaze.Application.UseCases.Admin.Codes.GetCode;

public sealed record GetCodeResult(int Id, Guid? GradeId, Guid? ToGradeId, string? Name, int TotalEx, int DiffEx);