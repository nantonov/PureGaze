using PureGaze.Domain.Entities;

namespace PureGaze.Application.Abstractions.Infrastructure;

public interface IAssessmentRepository
{
    Task<Assessment?> GetByIdAsync(int id, CancellationToken ct = default);
    Task AddAsync(Assessment assessment, CancellationToken ct = default);
}
