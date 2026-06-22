using Microsoft.EntityFrameworkCore;
using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Domain.Entities;
using PureGaze.Domain.Enums;

namespace PureGaze.Infrastructure.Database.Repositories;

public class AssessmentRepository(AppDbContext context) : IAssessmentRepository
{
    public async Task<Assessment?> GetByIdAsync(int id, CancellationToken ct = default)
        => await context.Assessments.Include(x => x.Stages).FirstOrDefaultAsync(x => x.Id == id, ct);

    public async Task AddAsync(Assessment assessment, CancellationToken ct = default)
        => await context.Assessments.AddAsync(assessment, ct);

    public async Task SaveChangesAsync(CancellationToken ct = default)
        => await context.SaveChangesAsync(ct);

    public async Task<IReadOnlyList<Assessment>> GetNewAssessmentsAsync(
        int levelOrder, int currentUserId, CancellationToken ct = default)
        => await context.Assessments
            .Include(a => a.Employee)
            .Include(a => a.Code!).ThenInclude(c => c.Grade)
            .Include(a => a.Code!).ThenInclude(c => c.ToGrade)
            .Include(a => a.Stages).ThenInclude(s => s.Topic!).ThenInclude(t => t.TopicTranslates)
            .Include(a => a.Stages).ThenInclude(s => s.Assessor)
            .Where(a => (a.Status == AssessmentStatus.Created || a.Status == AssessmentStatus.InProgress)
                     && (a.Code!.ToGrade!.OrderValue <= levelOrder || a.EmployeeId == currentUserId))
            .OrderByDescending(a => a.CreatedAt)
            .AsSplitQuery()
            .AsNoTracking()
            .ToListAsync(ct);

    public async Task<(IReadOnlyList<Assessment> Items, int Total)> GetHistoryAssessmentsAsync(
        string? search, int page, int pageSize, CancellationToken ct = default)
    {
        IQueryable<Assessment> q = ApplySearch(context.Assessments
            .Include(a => a.Employee)
            .Include(a => a.Code!).ThenInclude(c => c.Grade)
            .Include(a => a.Code!).ThenInclude(c => c.ToGrade)
            .Where(a => a.Status == AssessmentStatus.Finished || a.Status == AssessmentStatus.Closed),
            search);

        return (await q.OrderByDescending(a => a.CreatedAt).Skip((page - 1) * pageSize).Take(pageSize)
                       .AsNoTracking().ToListAsync(ct),
                await q.CountAsync(ct));
    }

    private static IQueryable<Assessment> ApplySearch(IQueryable<Assessment> q, string? search)
        => string.IsNullOrWhiteSpace(search) ? q : q.Where(a =>
            a.Employee!.Email!.Contains(search) ||
            a.Employee!.FirstNameEn!.Contains(search) ||
            a.Employee!.LastNameEn!.Contains(search));
}
