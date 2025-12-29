using Assessment.Application.Abstractions.Infrastructure;
using Common.Domain.Entities;
using Common.Data.Enums;

namespace Assessment.Infrastructure.Factories;

public class EmailFactory : IEmailFactory
{
    private const string FromEmailDefault = "puregaze.info@gmail.com";

    public Email CreateAssessmentRequestEmail(string managerEmail, string employeeName)
        => CreateEmail(managerEmail, $"{employeeName} has created a new assessment request");
    
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
