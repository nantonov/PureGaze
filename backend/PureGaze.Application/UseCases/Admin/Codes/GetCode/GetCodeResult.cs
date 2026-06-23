using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Admin.Codes.GetCode;

public sealed record GetCodeResult(int Id, Guid? GradeId, Guid? ToGradeId, string? Name, int TotalEx, int DiffEx)
{
    public static GetCodeResult ToResult(Code code)
        => new(code.Id, code.GradeId, code.ToGradeId, code.Name, code.TotalEx, code.DiffEx);
}
