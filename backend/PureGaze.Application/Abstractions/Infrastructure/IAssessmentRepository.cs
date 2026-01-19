using PureGaze.Domain.Entities;

namespace PureGaze.Application.Abstractions.Infrastructure;

public interface IAssessmentRepository
{
    Task AddAsync(Assessment assessment, CancellationToken ct = default);
    Task<Assessment?> GetByIdAsync(int id, CancellationToken ct = default);
}
