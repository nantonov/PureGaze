using PureGaze.Domain.Entities;

namespace PureGaze.Application.Abstractions.Infrastructure;

public interface ITopicTranslatesRepository
{
    Task<IReadOnlyList<TopicTranslate>> GetTopicTranslatesAsync(int topicId, CancellationToken ct = default);
    Task AddAsync(TopicTranslate topicTranslate, CancellationToken ct = default);
    Task DeleteTranslatesForTopicAsync(int topicId, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}
