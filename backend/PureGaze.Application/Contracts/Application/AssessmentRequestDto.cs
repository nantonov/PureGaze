using System.Text.Json.Serialization;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.Contracts.Application;

public class AssessmentRequestDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("employeeFullName")]
    public string? EmployeeFullName { get; set; }

    [JsonPropertyName("managerFullName")]
    public string? ManagerFullName { get; set; }

    [JsonPropertyName("code")]
    public string? Code { get; set; }

    [JsonPropertyName("status")]
    public AssessmentRequestStatus Status { get; set; }
}