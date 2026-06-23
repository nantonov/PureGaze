using System.Text.Json.Serialization;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Admin.Employees.GetEmployees;

public sealed record GetEmployeesResult(int Total, IReadOnlyList<GetEmployeesDto> Employees);

public sealed class GetEmployeesDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("fullName")]
    public string? FullName { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("managerLevel")]
    public string? ManagerLevel { get; set; }

    [JsonPropertyName("m1")]
    public string? M1 { get; set; }

    [JsonPropertyName("m2")]
    public string? M2 { get; set; }

    [JsonPropertyName("m3")]
    public string? M3 { get; set; }

    public static GetEmployeesDto ToDto(Employee employee)
        => new()
        {
            Id = employee.Id,
            FullName = $"{employee.FirstNameEn} {employee.LastNameEn}",
            Email = employee.Email,
            ManagerLevel = employee.ManagerialLevel?.Value,
            M1 = $"{employee.M1?.FirstNameEn} {employee.M1?.LastNameEn}",
            M2 = $"{employee.M2?.FirstNameEn} {employee.M2?.LastNameEn}",
            M3 = $"{employee.M3?.FirstNameEn} {employee.M3?.LastNameEn}",
        };
}
