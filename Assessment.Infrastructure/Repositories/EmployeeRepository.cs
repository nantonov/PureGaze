using Assessment.Application.Abstractions.Infrastructure;
using Common.DAL;
using Common.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Assessment.Infrastructure.Repositories;

public class EmployeeRepository(AppDbContext context) : IEmployeeRepository
{
    public async Task<Employee?> GetEmployeeAsync(int id, CancellationToken ct = default)
        => await context.Employees.FirstOrDefaultAsync(e => e.Id == id, ct);
}
