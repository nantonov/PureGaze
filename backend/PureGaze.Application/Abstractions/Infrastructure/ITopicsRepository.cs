using PureGaze.Domain.Entities;

namespace PureGaze.Application.Abstractions.Infrastructure;

public interface ITopicsRepository
{
    Task<Topic?> GetByIdAsync(int topicId, CancellationToken ct = default);
    IAsyncEnumerable<Topic> QueryByTemplateAsync(int templateId, int page, int pageSize);
    Task AddAsync(Topic topic, CancellationToken ct = default);
    void Delete(Topic topic);
    Task SaveChangesAsync(CancellationToken ct = default);
}
