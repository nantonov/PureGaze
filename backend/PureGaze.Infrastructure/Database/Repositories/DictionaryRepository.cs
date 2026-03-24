using Microsoft.EntityFrameworkCore;
using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Domain.Entities;

namespace PureGaze.Infrastructure.Database.Repositories;

public class DictionaryRepository<T>(AppDbContext dbContext)
    : IDictionaryRepository<T>
    where T : BaseDictionaryEntity
{
    public async Task<List<T>> GetAllAsync(CancellationToken ct = default)
        => await dbContext.Set<T>().ToListAsync(ct);

    public async Task<IDictionary<Guid, T>> GetByIdsAsync(IReadOnlyList<Guid> ids, CancellationToken ct = default)
        => await dbContext.Set<T>()
            .Where(x => ids.Contains(x.Id))
            .ToDictionaryAsync(x => x.Id, ct);

    public async Task AddAsync(T dictionary, CancellationToken ct = default)
        => await dbContext.AddAsync(dictionary, ct);

    public async Task SaveChangesAsync(CancellationToken ct = default)
        => await dbContext.SaveChangesAsync(ct);
}