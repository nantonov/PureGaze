using PureGaze.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Domain.Enums;

namespace PureGaze.Infrastructure.Database.Repositories;

public class AssessmentRequestRepository(AppDbContext context)
    : IAssessmentRequestRepository
{
    public async Task<IReadOnlyList<AssessmentRequest>> GetByEmployeeEmailAsync(string employeeEmail, int page, int pageSize, CancellationToken ct = default)
        => await context.AssessmentRequests
            .Include(x => x.Employee)
            .Include(x => x.Manager)
            .Include(x => x.Code)
            .Where(x => x.Employee != null && x.Employee.Email == employeeEmail)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync(ct);

    public async Task<IReadOnlyList<AssessmentRequest>> GetByManagerEmailAsync(string managerEmail, int page, int pageSize, CancellationToken ct = default)
        => await context.AssessmentRequests
            .Include(x => x.Employee)
            .Include(x => x.Manager)
            .Include(x => x.Code)
            .Where(x => x.Manager != null && x.Manager.Email == managerEmail)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync(ct);

    public async Task<AssessmentRequest?> GetEmployeeActiveAssessmentRequest(int employeeId, CancellationToken ct = default)
        => await context.AssessmentRequests
            .FirstOrDefaultAsync(
                x => x.EmployeeId == employeeId
                     && x.Status == AssessmentRequestStatus.Created
                     || x.Status == AssessmentRequestStatus.UnderReview, ct);

    public async Task<AssessmentRequest?> GetByIdWithEmployeeAsync(int id, CancellationToken ct = default)
    => await context.AssessmentRequests
            .Include(r => r.Employee)
            .FirstOrDefaultAsync(r => r.Id == id, ct);

    public async Task<AssessmentRequest?> GetByIdAsync(int id, CancellationToken ct = default)
        => await context.AssessmentRequests
            .Include(x => x.Employee)
            .Include(x => x.Manager)
            .Include(x => x.Code)
            .FirstOrDefaultAsync(x => x.Id == id, ct);

    public async Task AddAsync(AssessmentRequest assessment, CancellationToken ct = default)
        => await context.AssessmentRequests.AddAsync(assessment, ct);

    public async Task<int> GetCountByManagerEmailAsync(string managerEmail, CancellationToken ct = default)
        => await context.AssessmentRequests.CountAsync(x=> x.Manager != null && x.Manager.Email == managerEmail, ct);

    public async Task<int> GetCountByEmployeeEmailAsync(string employeeEmail, CancellationToken ct = default)
        => await context.AssessmentRequests.CountAsync(x=> x.Employee != null && x.Employee.Email == employeeEmail, ct);

    public async Task SaveChangesAsync(CancellationToken ct = default)
        => await context.SaveChangesAsync(ct);
}
