using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Admin.Codes.GetAllCodes;

public sealed record GetAllCodesResult(int Id, string? Name, string? Display)
{
    public static GetAllCodesResult ToResult(Code code)
        => new(code.Id, code.Name, $"{code.Grade?.Translation} -> {code.ToGrade?.Translation}");
}
