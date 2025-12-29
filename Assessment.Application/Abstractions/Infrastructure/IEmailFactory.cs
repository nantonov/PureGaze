using Common.Domain.Entities;

namespace Assessment.Application.Abstractions.Infrastructure;

public interface IEmailFactory
{
    Email CreateAssessmentRequestEmail(string managerEmail, string employeeName);
}
