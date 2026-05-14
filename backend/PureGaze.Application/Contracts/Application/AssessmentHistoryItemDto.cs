using System.Text.Json.Serialization;

namespace PureGaze.Application.Contracts.Application;

public class AssessmentHistoryItemDto
{
    [JsonPropertyName("id")] 
    public int Id { get; set; }

    [JsonPropertyName("employeeFullName")] 
    public string EmployeeFullName { get; set; } = string.Empty;

    [JsonPropertyName("employeeEmail")] 
    public string EmployeeEmail { get; set; } = string.Empty;
    
    [JsonPropertyName("gradeRange")] 
    public string GradeRange { get; set; } = string.Empty;

    [JsonPropertyName("status")] 
    public string Status { get; set; } = string.Empty;
    
    [JsonPropertyName("createdAt")] 
    public DateTime CreatedAt { get; set; }
}
