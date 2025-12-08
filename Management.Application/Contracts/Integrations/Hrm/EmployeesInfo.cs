using System.Text.Json.Serialization;

namespace Management.Application.Contracts.Integrations.Hrm;

public class EemployeeDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("firstNameEn")]
    public string? FirstNameEn { get; set; }

    [JsonPropertyName("lastNameEn")]
    public string? LastNameEn { get; set; }
    
    [JsonPropertyName("professionalLevelId")]
    public Guid? ProfessionalLevelId { get; set; }
    
    [JsonPropertyName("managerialLevelId")]
    public Guid? ManagerialLevelId { get; set; }
    
    [JsonPropertyName("email")]
    public string? Email { get; set; }
    
    [JsonPropertyName("managerId")]
    public int? ManagerId { get; set; }

    [JsonPropertyName("manager")]
    public int? HeadId { get; set; }
    
    [JsonPropertyName("resourceManager")]
    public int? RMId { get; set; }
    
    [JsonPropertyName("managerM1")]
    public int? M1Id { get; set; }
    
    [JsonPropertyName("managerM2")]
    public int? M2Id { get; set; }
    
    [JsonPropertyName("managerM3")]
    public int? M3Id { get; set; }
    
    [JsonPropertyName("managerM4")]
    public int? M4Id { get; set; }
}