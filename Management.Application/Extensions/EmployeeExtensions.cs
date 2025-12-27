using Common.Domain.Entities;
using Management.Application.Contracts.Integrations.Hrm;

namespace Management.Application.Extensions;

public static class EmployeeExtensions
{
    public static Employee ToEntity(this EmployeeDto dto) 
        => new Employee
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
            LifecycleStatus =  dto.LifecycleStatus
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