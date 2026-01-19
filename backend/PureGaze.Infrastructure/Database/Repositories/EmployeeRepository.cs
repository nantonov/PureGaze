using PureGaze.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using PureGaze.Application.Abstractions.Infrastructure;

namespace PureGaze.Infrastructure.Database.Repositories;

public class EmployeeRepository(AppDbContext dbContext)
    : IEmployeeRepository
{
    public async Task<IDictionary<int, Employee>> GetByIdsAsync(IReadOnlyList<int> ids, CancellationToken ct = default)
        => await dbContext
            .Employees
            .Where(x => ids.Contains(x.Id))
            .ToDictionaryAsync(x => x.Id, ct);
    
    public async Task<Employee?> GetByIdAsync(int id, CancellationToken ct = default)
        => await dbContext.Employees
            .Include(x => x.M1)
            .Include(x => x.M2)
            .Include(x => x.M3)
            .Include(x => x.M4)
            .Include(x => x.Manager)
            .FirstOrDefaultAsync(x => x.Id == id, ct);

    public async Task AddAsync(Employee employee, CancellationToken ct = default)
        => await dbContext.Employees.AddAsync(employee, ct);

    public async Task SaveChangesAsync(CancellationToken ct = default)
        => await dbContext.SaveChangesAsync(ct);
}