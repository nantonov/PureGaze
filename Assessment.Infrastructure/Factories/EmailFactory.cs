using Assessment.Application.Abstractions.Infrastructure;
using Common.Domain.Entities;
using Common.Data.Enums;

namespace Assessment.Infrastructure.Factories;

public class EmailFactory : IEmailFactory
{
    private const string FromEmailDefault = "puregazeinfo@gmail.com";

    public Email CreateEmailForM(string managerEmail, string employeeName, DateTime requestedDate)
    {
        var timeToDeadline = requestedDate - DateTime.UtcNow;
        var priority = GetPriority(timeToDeadline);
        
        var body = $"Employee {employeeName} has created a new assessment request for {requestedDate:f}. M1 approval is required.";

        return CreateEmail(managerEmail, body, priority);
    }

    public Email CreateEmailForEmployee(string employeeEmail, DateTime requestedDate)
    {
        var timeToDeadline = requestedDate - DateTime.UtcNow;
        var priority = GetPriority(timeToDeadline);
        
        var body = $"You have a new assessment scheduled for {requestedDate:f}.";

        return CreateEmail(employeeEmail, body, priority);
    }

    private Email CreateEmail(string to, string body, EmailPriority priority)
    {
        return new Email
        {
            Id = Guid.NewGuid(),
            Subject = "Assessment notification",
            Body = body,
            To = to,
            From = FromEmailDefault,
            Priority = priority,
            
            Status = EmailStatus.InQueue,
            RetryCount = 0,
            SentAt = null,
            ErrorMessage = null
        };
    }

    private EmailPriority GetPriority(TimeSpan timeToDeadline)
    {
        return timeToDeadline switch
        {
            _ when timeToDeadline <= TimeSpan.FromHours(3) 
                => EmailPriority.High,
            _ when timeToDeadline <= TimeSpan.FromHours(24)
                => EmailPriority.Normal,
            _ 
                => EmailPriority.Low
        };
    }
}
