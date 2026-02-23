using PureGaze.Domain.Entities;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.Abstractions.Infrastructure;

public interface ITopicTranslatesRepository
{
    Task<IReadOnlyList<TopicTranslate>> GetTopicsTranslatesAsync(int topicId, int page, int pageSize, CancellationToken ct = default);
    Task<TopicTranslate?> GetByTopicIdAndLanguageAsync(int topicId, Language language, CancellationToken ct = default);
    Task AddAsync(TopicTranslate topicTranslate, CancellationToken ct = default);
    void Delete(TopicTranslate topicTranslate);
    Task SaveChangesAsync(CancellationToken ct = default);
}
