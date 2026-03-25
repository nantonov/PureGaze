using PureGaze.Domain.Entities;

namespace PureGaze.Application.Abstractions.Infrastructure;

public interface IEmployeeRepository
{
    Task<IReadOnlyList<Employee>> GetEmployeesAsync(int page, int pageSize, CancellationToken ct = default);
    Task<IDictionary<int, Employee>> GetByIdsAsync(IReadOnlyList<int> ids, CancellationToken ct = default);
    Task<Employee?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<Employee?> GetByEmailAsync(string email, CancellationToken ct = default);
    Task AddAsync(Employee employee, CancellationToken ct = default);
    Task<int> GetCountAsync(CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}