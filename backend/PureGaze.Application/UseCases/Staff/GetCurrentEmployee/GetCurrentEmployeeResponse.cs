using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Staff.GetCurrentEmployee;

public sealed record GetCurrentEmployeeResponse(
    int Id, 
    string? FirstName, 
    string? LastName, 
    string? Email, 
    string? ManagerLevel)
{
    public static GetCurrentEmployeeResponse ToResult(Employee empl)
        => new(empl.Id, empl.FirstNameEn, empl.LastNameEn, empl.Email, empl.ManagerialLevel?.Value);
}