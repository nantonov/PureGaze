using PureGaze.Application.Contracts.Application;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.Extensions;

public static class AssessmentRequestExtensions
{
    public static AssessmentRequestDetailsDto ToDetailsDto(this AssessmentRequest assessmentRequest)
        => new()
        {
            Id = assessmentRequest.Id,
            EmployeeId = assessmentRequest.EmployeeId,
            EmployeeFullName = $"{assessmentRequest.Employee.FirstNameEn} {assessmentRequest.Employee.LastNameEn}".Trim(),
            ManagerId = assessmentRequest.ManagerId,
            ManagerFullName = $"{assessmentRequest.Manager.FirstNameEn} {assessmentRequest.Manager.LastNameEn}".Trim(),
            Code = assessmentRequest.Code.Display,
            Status = assessmentRequest.Status,
            RejectionReason = assessmentRequest.RejectionReason  
        };
    
    public static AssessmentRequestDto ToDto(this AssessmentRequest assessmentRequest)
        => new()
        {
            Id = assessmentRequest.Id,
            EmployeeFullName = $"{assessmentRequest.Employee.FirstNameEn} {assessmentRequest.Employee.LastNameEn}".Trim(),
            ManagerFullName = $"{assessmentRequest.Manager.FirstNameEn} {assessmentRequest.Manager.LastNameEn}".Trim(),
            Code = assessmentRequest.Code.Display,
            Status = assessmentRequest.Status,
        };
}