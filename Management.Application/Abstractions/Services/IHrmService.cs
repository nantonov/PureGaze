using Management.Application.Contracts.Integrations.Hrm;

namespace Management.Application.Abstractions.Services;

public interface IHrmService
{
    IAsyncEnumerable<EemployeeDto> GetEmployeesAsync();
    Task<DictionariesDto?> GetDictionariesAsync();
}