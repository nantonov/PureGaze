using Assessment.Application.Contracts.Application;
using Common.Domain.Entities;

namespace Assessment.Application.Extensions;

public static class AssessmentRequestExtensions
{
    public static AssessmentRequestDetailsDto ToDto(this AssessmentRequest assessmentRequest) 
        => new(
            assessmentRequest.Id,
            assessmentRequest.EmployeeId,
            $"{assessmentRequest.Employee.FirstNameEn} {assessmentRequest.Employee.LastNameEn}".Trim(),
            assessmentRequest.ManagerId,
            $"{assessmentRequest.Manager.FirstNameEn} {assessmentRequest.Manager.LastNameEn}".Trim(),
            assessmentRequest.Code.Display,
            assessmentRequest.Status,
            assessmentRequest.RejectionReason
        );
    
}