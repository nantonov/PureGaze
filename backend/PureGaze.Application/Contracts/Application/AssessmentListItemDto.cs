using System.Text.Json.Serialization;

namespace PureGaze.Application.Contracts.Application;

public class AssessmentListItemDto
{
    [JsonPropertyName("id")] 
    public int Id { get; set; }

    [JsonPropertyName("employeeFullName")] 
    public string EmployeeFullName { get; set; } = string.Empty;

    [JsonPropertyName("employeeEmail")] 
    public string EmployeeEmail { get; set; } = string.Empty;

    [JsonPropertyName("gradeRange")] 
    public string GradeRange { get; set; } = string.Empty;
    
    [JsonPropertyName("isOwnAssessment")]
    public bool IsOwnAssessment { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("stages")]
    public List<AssessmentStageItemDto> Stages { get; set; } = [];
}
