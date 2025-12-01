using System.Text.Json.Serialization;

namespace Management.Infrastructure.Integrations.Hrm.Responses;

public class GetEmployeesResponse
{
    [JsonPropertyName("employeeManagerIdList")]
    public IList<EemployeeManagerId>? EmployeeManagerIdList { get; set; }
}

public class EemployeeManagerId
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