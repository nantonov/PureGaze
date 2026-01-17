using System.Text.Json.Serialization;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.Contracts.Application;

public class AssessmentRequestDetailsDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("employeeId")]
    public int EmployeeId { get; set; }
    
    [JsonPropertyName("employeeFullName")]
    public string? EmployeeFullName { get; set; }
    
    [JsonPropertyName("managerId")]
    public int ManagerId { get; set; }
    
    [JsonPropertyName("managerFullName")]
    public string? ManagerFullName { get; set; }
    
    [JsonPropertyName("code")]
    public string? Code { get; set; }
    
    [JsonPropertyName("status")]
    public AssessmentRequestStatus Status { get; set; }
    
    [JsonPropertyName("rejectionReason")]
    public string? RejectionReason { get; set; }
}
