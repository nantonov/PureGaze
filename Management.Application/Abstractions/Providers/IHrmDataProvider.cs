using Management.Application.Contracts.Integrations.Hrm;

namespace Management.Application.Abstractions.Providers;

public interface IHrmDataProvider
{
    IAsyncEnumerable<IReadOnlyList<EmployeeDto>> GetEmployeesAsync(CancellationToken ct);
    Task<DictionariesDto?> GetDictionariesAsync(CancellationToken ct);
}