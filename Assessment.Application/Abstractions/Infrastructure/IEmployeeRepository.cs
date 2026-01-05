using Common.Domain.Entities;

namespace Assessment.Application.Abstractions.Infrastructure;

public interface IEmployeeRepository
{
    Task<Employee?> GetEmployeeAsync(int id, CancellationToken ct = default);
}