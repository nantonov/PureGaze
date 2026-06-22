using PureGaze.Domain.Entities;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.UseCases.Staff.GetCurrentEmployee;

public sealed record GetCurrentEmployeeResult(
    int Id,
    string? FirstName,
    string? LastName,
    string? Email,
    string? ManagerLevel,
    Language? Language)
{
    public static GetCurrentEmployeeResult ToResult(Employee empl)
        => new(
            empl.Id,
            empl.FirstNameEn,
            empl.LastNameEn,
            empl.Email,
            empl.ManagerialLevel?.Value,
            empl.EmployeeSettings?.Language
        );
}