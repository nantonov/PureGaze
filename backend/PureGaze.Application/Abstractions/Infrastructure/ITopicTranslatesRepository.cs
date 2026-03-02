using PureGaze.Domain.Entities;

namespace PureGaze.Application.Abstractions.Infrastructure;

public interface ITopicTranslatesRepository
{
    Task<IReadOnlyList<TopicTranslate>> GetTopicsTranslatesAsync(int topicId, CancellationToken ct = default);
    Task AddAsync(TopicTranslate topicTranslate, CancellationToken ct = default);
    Task DeleteTranslatesForTopicAsync(int topicId, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}
