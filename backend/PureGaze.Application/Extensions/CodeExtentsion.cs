using PureGaze.Application.Contracts.Application;
using PureGaze.Domain.Entities;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.Extensions;

public static class CodeExtentsion
{
    public static CodeDto ToDto(this Code code)
        => new(
            code.Id,
            code.GradeId,
            code.ToGradeId,
            code.Display,
            code.TotalEx,
            code.DiffEx,
            code.CodeTranslates.FirstOrDefault(t => t.Language == Language.Ru)?.LevelVision,
            code.CodeTranslates.FirstOrDefault(t => t.Language == Language.En)?.LevelVision
        );
}