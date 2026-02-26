using PureGaze.Domain.Entities;

namespace PureGaze.Application.Abstractions.Infrastructure;

public interface IEmailFactory
{
    Email CreateAssessmentRequestEmail(string managerEmail, string employeeName);
    Email CreateAssessmentApprovedEmail(string employeeEmail, string employeeName);
    Email CreateAssessmentRejectedEmail(string employeeEmail, string employeeName, string rejectionReason);
    Email CreateAssessmentDirectByManagerEmail(string employeeEmail, string employeeName);
    Email CreateAssessmentStageAssignEmail(string employeeEmail, string managerName, string stageName);
}