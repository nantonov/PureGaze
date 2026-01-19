using PureGaze.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using PureGaze.Application.Abstractions.Infrastructure;

namespace PureGaze.Infrastructure.Database.Repositories;

public class AssessmentRequestRepository(AppDbContext context) 
    : IAssessmentRequestRepository
{
    public async Task<IReadOnlyList<AssessmentRequest>> GetByEmployeeIdAsync(int employeeId, int page, int pageSize, CancellationToken ct = default)
        => await context.AssessmentRequests
            .Include(x => x.Employee)
            .Include(x => x.Manager)
            .Include(x => x.Code)
            .Where(x => x.EmployeeId == employeeId)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync(ct);

    public async Task<IReadOnlyList<AssessmentRequest>> GetByManagerIdAsync(int managerId, int page, int pageSize, CancellationToken ct = default)
        => await context.AssessmentRequests
            .Include(x => x.Employee)
            .Include(x => x.Manager)
            .Include(x => x.Code)
            .Where(x => x.ManagerId == managerId)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync(ct);

    public async Task<AssessmentRequest?> GetByIdWithEmployeeAsync(int id, CancellationToken ct)
    {
        return await context.AssessmentRequests
            .Include(r => r.Employee)
            .FirstOrDefaultAsync(r => r.Id == id, ct);
    }

    public async Task<AssessmentRequest?> GetByIdAsync(int id, CancellationToken ct = default)
        => await context.AssessmentRequests
            .Include(x => x.Employee)
            .Include(x => x.Manager)
            .Include(x => x.Code)
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    
    public async Task AddAsync(AssessmentRequest assessment, CancellationToken ct = default) 
        => await context.AssessmentRequests.AddAsync(assessment, ct);
    
    public async Task SaveChangesAsync(CancellationToken ct = default)
        => await context.SaveChangesAsync(ct);
}
