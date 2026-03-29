using PureGaze.Application.Contracts.Application;
using PureGaze.Application.UseCases.Admin.Codes.GetCode;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.Extensions;

public static class CodeExtentsion
{
    public static CodeDto ToDto(this Code code)
        => new(
            code.Id,
            code.Name,
            $"{code.Grade?.Translation} -> {code.ToGrade?.Translation}");

    public static GetCodeResult ToGetCodeResult(this Code code)
        => new(
            code.Id,
            code.GradeId,
            code.ToGradeId,
            code.Name,
            code.TotalEx,
            code.DiffEx);
}