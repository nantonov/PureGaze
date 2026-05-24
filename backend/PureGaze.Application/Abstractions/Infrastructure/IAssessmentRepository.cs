using PureGaze.Domain.Entities;

namespace PureGaze.Application.Abstractions.Infrastructure;

public interface IAssessmentRepository
{
    Task<Assessment?> GetByIdAsync(int id, CancellationToken ct = default);
    Task AddAsync(Assessment assessment, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
    Task<IReadOnlyList<Assessment>> GetNewAssessmentsAsync(
        int levelOrder, int currentUserId, CancellationToken ct = default);
    Task<(IReadOnlyList<Assessment> Items, int Total)> GetHistoryAssessmentsAsync(
        string? search, int page, int pageSize, CancellationToken ct = default);
}
