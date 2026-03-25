using PureGaze.Application.Contracts.Integrations.Hrm;

namespace PureGaze.Application.Abstractions.Providers;

public interface IHrmDataProvider
{
    IAsyncEnumerable<IReadOnlyList<HrmEmployeeDto>> GetEmployeesAsync(CancellationToken ct);
    Task<HrmDictionariesDto?> GetDictionariesAsync(CancellationToken ct);
}