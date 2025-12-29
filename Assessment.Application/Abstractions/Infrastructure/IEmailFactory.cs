using Common.Domain.Entities;

namespace Assessment.Application.Abstractions.Infrastructure;

public interface IEmailFactory
{
    Email CreateEmailForM(string managerEmail, string employeeName, DateTime requestedDate);
    Email CreateEmailForEmployee(string employeeEmail, DateTime requestedDate);
}
