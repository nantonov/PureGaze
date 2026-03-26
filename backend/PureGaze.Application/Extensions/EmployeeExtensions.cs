using PureGaze.Application.Contracts.Application;
using PureGaze.Domain.Entities;
using PureGaze.Application.Contracts.Integrations.Hrm;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.Extensions;

public static class EmployeeExtensions
{
    public static Employee ToEntity(this HrmEmployeeDto dto)
        => new()
        {
            Id = dto.Id,
            UpdatedAt = DateTime.UtcNow,
            FirstNameEn = dto.FirstNameEn,
            LastNameEn = dto.LastNameEn,
            ProfessionalLevelId = dto.ProfessionalLevelId,
            ManagerialLevelId = dto.ManagerialLevelId,
            Email = dto.Email,
            ManagerId = dto.ManagerId,
            HeadId = dto.HeadId,
            RMId = dto.RMId,
            M1Id = dto.M1Id,
            M2Id = dto.M2Id,
            M3Id = dto.M3Id,
            M4Id = dto.M4Id,
            Hash = dto.Hash,
            HireDate = dto.HireDate,
            TerminationDate = dto.TerminationDate,
            LifecycleStatus = dto.LifecycleStatus,
            EmployeeSettings = new EmployeeSettings
            {
                Language = Language.En
            }
        };

    public static void Update(this Employee emp, HrmEmployeeDto dto)
    {
        emp.Id = dto.Id;
        emp.UpdatedAt = DateTime.UtcNow;
        emp.FirstNameEn = dto.FirstNameEn;
        emp.LastNameEn = dto.LastNameEn;
        emp.ProfessionalLevelId = dto.ProfessionalLevelId;
        emp.ManagerialLevelId = dto.ManagerialLevelId;
        emp.Email = dto.Email;
        emp.ManagerId = dto.ManagerId;
        emp.HeadId = dto.HeadId;
        emp.RMId = dto.RMId;
        emp.M1Id = dto.M1Id;
        emp.M2Id = dto.M2Id;
        emp.M3Id = dto.M3Id;
        emp.M4Id = dto.M4Id;
        emp.Hash = dto.Hash;
        emp.HireDate = dto.HireDate;
        emp.TerminationDate = dto.TerminationDate;
        emp.LifecycleStatus = dto.LifecycleStatus;
    }

    public static EmployeeDto ToDto(this Employee emp)
        => new()
        {
            Id = emp.Id,
            FullName = $"{emp.FirstNameEn} {emp.LastNameEn}",
            Email = emp.Email,
            ManagerLevel = emp.ManagerialLevel?.Value,
            M1 = $"{emp.M1?.FirstNameEn} {emp.M1?.LastNameEn}",
            M2 = $"{emp.M2?.FirstNameEn} {emp.M2?.LastNameEn}",
            M3 = $"{emp.M3?.FirstNameEn} {emp.M3?.LastNameEn}",
        };
}