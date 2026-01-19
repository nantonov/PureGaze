using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Domain.Entities;
using PureGaze.Domain.Enums;

namespace PureGaze.Infrastructure.Notifications.Emails;

public class EmailFactory : IEmailFactory
{
    private const string FromEmailDefault = "puregaze.info@gmail.com";

    public Email CreateAssessmentRequestEmail(string managerEmail, string employeeName)
        => CreateEmail(managerEmail, $"{employeeName} has created a new assessment request");

    public Email CreateAssessmentApprovedEmail(string employeeEmail, string employeeFirstName, string employeeLastName)
        => CreateEmail(employeeEmail, $"{employeeFirstName} {employeeLastName}, your assessment has been successfully approved");
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