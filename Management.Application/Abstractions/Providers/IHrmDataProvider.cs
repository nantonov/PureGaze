using System.Runtime.CompilerServices;
using Management.Application.Contracts.Integrations.Hrm;

namespace Management.Application.Abstractions.Providers;

public interface IHrmDataProvider
{
    IAsyncEnumerable<IReadOnlyList<EemployeeDto>> GetEmployeesAsync(CancellationToken ct);
    Task<DictionariesDto?> GetDictionariesAsync(CancellationToken ct);
}