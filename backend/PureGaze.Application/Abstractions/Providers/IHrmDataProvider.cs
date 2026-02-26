using PureGaze.Application.Contracts.Integrations.Hrm;

namespace PureGaze.Application.Abstractions.Providers;

public interface IHrmDataProvider
{
    IAsyncEnumerable<IReadOnlyList<EmployeeDto>> GetEmployeesAsync(CancellationToken ct);
    Task<DictionariesDto?> GetDictionariesAsync(CancellationToken ct);
}