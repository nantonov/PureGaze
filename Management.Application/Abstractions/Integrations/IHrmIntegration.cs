using Management.Application.Contracts.Integrations.Hrm;

namespace Management.Application.Abstractions.Intergrations;

public interface IHrmIntegration
{
    IAsyncEnumerable<EemployeeDto> GetEmployeesAsync();
    Task<DictionariesDto?> GetDictionariesAsync();
}