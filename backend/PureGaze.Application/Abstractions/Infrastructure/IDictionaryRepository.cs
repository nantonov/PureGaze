using PureGaze.Domain.Entities;

namespace PureGaze.Application.Abstractions.Infrastructure;

public interface IDictionaryRepository<T>
    where T : BaseDictionaryEntity
{
    Task<List<T>> GetAllAsync(CancellationToken ct = default);
    Task<IDictionary<Guid, T>> GetByIdsAsync(IReadOnlyList<Guid> ids, CancellationToken ct = default);
    Task AddAsync(T dictionary, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}