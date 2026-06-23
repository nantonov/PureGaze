using System.Text.Json.Serialization;
using PureGaze.Application.Contracts.Integrations.Hrm;
using PureGaze.Infrastructure.Helpers;

namespace PureGaze.Infrastructure.Integrations.Hrm.Responses;

public class GetEmployeesResult
{
    [JsonPropertyName("content")]
    public IList<HrmEemployee>? Eemployees { get; set; } = [];

    [JsonPropertyName("totalPages")]
    public int TotalPages { get; set; }
}

public class HrmEemployee
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

    [JsonPropertyName("manager")]
    public HrmManager? Manager { get; set; }

    [JsonPropertyName("head")]
    public HrmManager? Head { get; set; }

    [JsonPropertyName("resourceManager")]
    public HrmManager? RM { get; set; }

    [JsonPropertyName("managerM1")]
    public HrmManager? M1 { get; set; }

    [JsonPropertyName("managerM2")]
    public HrmManager? M2 { get; set; }

    [JsonPropertyName("managerM3")]
    public HrmManager? M3 { get; set; }

    [JsonPropertyName("managerM4")]
    public HrmManager? M4 { get; set; }

    [JsonPropertyName("hireDate")]
    public DateTime HireDate { get; set; }

    [JsonPropertyName("terminationDate")]
    public DateTime? TerminationDate { get; set; }

    [JsonPropertyName("lifecycleStatus")]
    public string? LifecycleStatus { get; set; }

    public static HrmEmployeeDto ToDto(HrmEemployee employee)
    {
        HrmEmployeeDto dto = new HrmEmployeeDto
        {
            Id = employee.Id,
            FirstNameEn = employee.FirstNameEn,
            LastNameEn = employee.LastNameEn,
            ProfessionalLevelId = employee.ProfessionalLevelId,
            ManagerialLevelId = employee.ManagerialLevelId,
            Email = employee.Email,
            ManagerId = employee.Manager?.Id,
            HeadId = employee.Head?.Id,
            RMId = employee.RM?.Id,
            M1Id = employee.M1?.Id,
            M2Id = employee.M2?.Id,
            M3Id = employee.M3?.Id,
            M4Id = employee.M4?.Id,
            HireDate = employee.HireDate,
            TerminationDate = employee.TerminationDate,
            LifecycleStatus = employee.LifecycleStatus,
        };

        dto.Hash = HashCalculationHelper.CalculateHash(dto);

        return dto;
    }
}

public class HrmManager
{
    [JsonPropertyName("id")]
    public int? Id { get; set; }
}
