using Microsoft.EntityFrameworkCore;
using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Domain.Entities;

namespace PureGaze.Infrastructure.Database.Repositories;

public class EmployeeRepository(AppDbContext dbContext)
    : IEmployeeRepository
{
    public async Task<IReadOnlyList<Employee>> GetEmployeesAsync(int page, int pageSize, CancellationToken ct = default)
        => await dbContext.Employees
            .Include(x => x.ManagerialLevel)
            .Include(x => x.M1)
            .Include(x => x.M2)
            .Include(x => x.M3)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync(ct);

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

    public async Task<Employee?> GetByEmailAsync(string email, CancellationToken ct = default)
        => await dbContext.Employees
            .Include(x => x.ManagerialLevel)
            .Include(x => x.EmployeeSettings)
            .Include(x => x.ProfessionalLevel)
            .Include(x => x.M1)
            .Include(x => x.M2)
            .Include(x => x.M3)
            .Include(x => x.M4)
            .Include(x => x.Manager)
            .FirstOrDefaultAsync(x => x.Email == email, ct);

    public async Task AddAsync(Employee employee, CancellationToken ct = default)
        => await dbContext.Employees.AddAsync(employee, ct);

    public async Task<int> GetCountAsync(CancellationToken ct = default)
        => await dbContext.Employees.CountAsync(ct);

    public async Task SaveChangesAsync(CancellationToken ct = default)
        => await dbContext.SaveChangesAsync(ct);
}