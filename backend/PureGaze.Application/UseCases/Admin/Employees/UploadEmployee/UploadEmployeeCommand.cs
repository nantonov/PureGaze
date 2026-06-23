using PureGaze.Application.Contracts.Integrations.Hrm;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.UseCases.Admin.Employees.UploadEmployee;

public sealed record UploadEmployeeCommand : IRequest
{
    public static Employee ToEntity(HrmEmployeeDto employee)
        => new()
        {
            Id = employee.Id,
            UpdatedAt = DateTime.UtcNow,
            FirstNameEn = employee.FirstNameEn,
            LastNameEn = employee.LastNameEn,
            ProfessionalLevelId = employee.ProfessionalLevelId,
            ManagerialLevelId = employee.ManagerialLevelId,
            Email = employee.Email,
            ManagerId = employee.ManagerId,
            HeadId = employee.HeadId,
            RMId = employee.RMId,
            M1Id = employee.M1Id,
            M2Id = employee.M2Id,
            M3Id = employee.M3Id,
            M4Id = employee.M4Id,
            Hash = employee.Hash,
            HireDate = employee.HireDate,
            TerminationDate = employee.TerminationDate,
            LifecycleStatus = employee.LifecycleStatus,
            EmployeeSettings = new EmployeeSettings { Language = Language.En }
        };

    public static void Update(Employee target, HrmEmployeeDto source)
    {
        target.Id = source.Id;
        target.UpdatedAt = DateTime.UtcNow;
        target.FirstNameEn = source.FirstNameEn;
        target.LastNameEn = source.LastNameEn;
        target.ProfessionalLevelId = source.ProfessionalLevelId;
        target.ManagerialLevelId = source.ManagerialLevelId;
        target.Email = source.Email;
        target.ManagerId = source.ManagerId;
        target.HeadId = source.HeadId;
        target.RMId = source.RMId;
        target.M1Id = source.M1Id;
        target.M2Id = source.M2Id;
        target.M3Id = source.M3Id;
        target.M4Id = source.M4Id;
        target.Hash = source.Hash;
        target.HireDate = source.HireDate;
        target.TerminationDate = source.TerminationDate;
        target.LifecycleStatus = source.LifecycleStatus;
    }
}
