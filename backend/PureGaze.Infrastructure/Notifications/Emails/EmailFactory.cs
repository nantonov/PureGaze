using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Domain.Entities;
using PureGaze.Domain.Enums;

namespace PureGaze.Infrastructure.Notifications.Emails;

public class EmailFactory : IEmailFactory
{
    private const string FromEmailDefault = "puregaze.info@gmail.com";

    public Email CreateAssessmentRequestEmail(string managerEmail, string employeeName)
        => CreateEmail(managerEmail, $"{employeeName} has created a new assessment request");

    public Email CreateAssessmentApprovedEmail(string employeeEmail, string employeeName)
        => CreateEmail(employeeEmail, $"{employeeName}, your assessment has been successfully approved");

    public Email CreateAssessmentRejectedEmail(string employeeEmail, string employeeName, string rejectionReason)
        => CreateEmail(employeeEmail, $"{employeeName}, your assessment request has been rejected. Reason: {rejectionReason}");

    public Email CreateAssessmentDirectByManagerEmail(string employeeEmail, string employeeName)
        => CreateEmail(employeeEmail, $"{employeeName}, your manager has created an assessment for you");

    private Email CreateEmail(string to, string body)
    {
        return new Email
        {
            Id = Guid.NewGuid(),
            Subject = "Assessment notification",
            Body = body,
            To = to,
            From = FromEmailDefault,

            Status = EmailStatus.InQueue
        };
    }
}