using Common.Domain.Entities;

namespace Assessment.Application.Abstractions.Infrastructure;

public interface IEmployeeRepository
{
    Task AddAsync(Employee employee, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
    Task<Employee?> GetEmployeeAsync(int id, CancellationToken ct = default);
}