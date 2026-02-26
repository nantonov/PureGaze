using PureGaze.Domain.Entities;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.UseCases.Staff.GetCurrentEmployee;

public sealed record GetCurrentEmployeeResponse(
    int Id,
    string? FirstName,
    string? LastName,
    string? Email,
    string? ManagerLevel,
    Language? Language)
{
    public static GetCurrentEmployeeResponse ToResult(Employee empl)
        => new(
            empl.Id,
            empl.FirstNameEn,
            empl.LastNameEn,
            empl.Email,
            empl.ManagerialLevel?.Value,
            empl.EmployeeSettings?.Language
        );
}