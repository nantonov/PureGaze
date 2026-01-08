using Common.Data.Enums;
    
namespace Assessment.Application.Contracts.Application;
    
public sealed class AssessmentRequestDetailsDto(
    int Id,
    int EmployeeId,
    string EmployeeFullName,
    int ManagerId, 
    string ManagerFullName,
    string Code,
    AssessmentRequestStatus Status,
    string? RejectionReason
);
