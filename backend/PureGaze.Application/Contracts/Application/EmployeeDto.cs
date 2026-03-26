using System.Text.Json.Serialization;

namespace PureGaze.Application.Contracts.Application;

public class EmployeeDto
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
}