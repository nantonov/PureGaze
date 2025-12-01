using Management.Application.Contracts.Integrations.Hrm;

namespace Management.Application.Abstractions.Services;

public interface IHrmService
{
    Task<EmployeesInfo?> GetEmployeesAsync();
    Task GetDictionariesAsync();
}