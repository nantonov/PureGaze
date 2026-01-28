using PureGaze.Domain.Entities;
using PureGaze.Application.Contracts.Integrations.Hrm;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.Extensions;

public static class EmployeeExtensions
{
    public static Employee ToEntity(this EmployeeDto dto) 
        => new()
        { 
            Id = dto.Id,
            UpdatedAt =  DateTime.UtcNow,
            FirstNameEn = dto.FirstNameEn,
            LastNameEn = dto.LastNameEn,
            ProfessionalLevelId =  dto.ProfessionalLevelId,
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
            LifecycleStatus =  dto.LifecycleStatus,
            EmployeeSettings = new EmployeeSettings
            {
                Theme = Theme.Dark,
                Language = Language.English
            }
        };
    
    public static void Update(this Employee emp, EmployeeDto dto)
    {
        emp.Id = dto.Id;
        emp.UpdatedAt =  DateTime.UtcNow;
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
}