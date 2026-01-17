using PureGaze.Domain.Entities;

namespace PureGaze.Application.Abstractions.Infrastructure;

public interface IEmailFactory
{
    Email CreateAssessmentRequestEmail(string managerEmail, string employeeName);
}
