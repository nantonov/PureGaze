using PureGaze.Domain.Entities;

namespace PureGaze.Application.Abstractions.Infrastructure;

public interface ITopicsRepository
{
    Task<IReadOnlyList<Topic>> GetTopicsByTemplateIdAsync(int templateId, int page, int pageSize, CancellationToken ct = default);
    Task<Topic?> GetByIdAsync(int id, CancellationToken ct = default);
    Task AddAsync(Topic topic, CancellationToken ct = default);
    void Delete(Topic topic);
    Task SaveChangesAsync(CancellationToken ct = default);
}
