using PureGaze.Domain.Entities;

namespace PureGaze.Application.Abstractions.Infrastructure;

public interface ITopicsRepository
{
    Task<Topic?> GetByIdAsync(int topicId, CancellationToken ct = default);
    Task<Topic?> GetByTemplateAsync(int templateId, CancellationToken ct = default);
    Task AddAsync(Topic topic, CancellationToken ct = default);
    void Delete(Topic topic);
    Task SaveChangesAsync(CancellationToken ct = default);
}
