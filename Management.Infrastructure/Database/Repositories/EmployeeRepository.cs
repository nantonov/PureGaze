using Common.DAL;
using Common.Domain.Entities;
using Management.Application.Abstractions.Database;
using Microsoft.EntityFrameworkCore;

namespace Management.Infrastructure.Database.Repositories;

public class EmployeeRepository(AppDbContext dbContext) 
    : IEmployeeRepository
{
    public async Task<IDictionary<int, Employee>> GetEmployeesByIdsAsync(IList<int> ids, CancellationToken ct)
        => await dbContext
            .Employees
            .Where(x => ids.Contains(x.Id))
            .ToDictionaryAsync(x => x.Id, ct);

    public async Task AddAsync(Employee employee, CancellationToken ct)
        => await dbContext.Employees.AddAsync(employee, ct);

    public async Task SaveChangesAsync(CancellationToken ct) 
        => await dbContext.SaveChangesAsync(ct);
}