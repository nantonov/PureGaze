using Common.Domain.Entities;

namespace Management.Application.Abstractions.Database;

public interface IEmployeeRepository
{
    Task<IDictionary<int, Employee>> GetEmployeesByIdsAsync(IList<int> ids, CancellationToken ct);
    Task AddAsync(Employee employee, CancellationToken ct);
    Task SaveChangesAsync(CancellationToken ct);
}