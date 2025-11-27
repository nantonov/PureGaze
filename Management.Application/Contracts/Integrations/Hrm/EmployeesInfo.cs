using System.Text.Json.Serialization;

namespace Management.Application.Contracts.Integrations.Hrm;

public class EmployeesInfo
{
    [JsonPropertyName("employees")]
    public IList<EemployeeDto>? Employees { get; set; }
}

public class EemployeeDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("firstNameRu")]
    public string? FirstNameRu { get; set; }

    [JsonPropertyName("lastNameRu")]
    public string? LastNameRu { get; set; }

    [JsonPropertyName("firstNameEn")]
    public string? FirstNameEn { get; set; }

    [JsonPropertyName("lastNameEn")]
    public string? LastNameEn { get; set; }
}