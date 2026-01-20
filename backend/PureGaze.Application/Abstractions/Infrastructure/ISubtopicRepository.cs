using PureGaze.Domain.Entities;

namespace PureGaze.Application.Abstractions.Infrastructure;

public interface ISubtopicRepository
{
    Task<Subtopic?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<string?> GetAnyExistingNameAsync(int topicId, IEnumerable<string> names, int? excludeSubtopicId = null, CancellationToken ct = default);
    Task AddAsync(Subtopic subtopic, CancellationToken ct = default);
    Task DeleteAsync(Subtopic subtopic, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}
