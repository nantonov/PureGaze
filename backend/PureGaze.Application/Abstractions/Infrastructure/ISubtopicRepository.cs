using PureGaze.Domain.Entities;

namespace PureGaze.Application.Abstractions.Infrastructure;

public interface ISubtopicRepository
{
    Task<Subtopic?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<bool> IsNameExistingAsync(int topicId, IEnumerable<string> names, int? excludeSubtopicId = null, CancellationToken ct = default);
    Task AddAsync(Subtopic subtopic, CancellationToken ct = default);
    void Delete(Subtopic subtopic);
    Task SaveChangesAsync(CancellationToken ct = default);
}
