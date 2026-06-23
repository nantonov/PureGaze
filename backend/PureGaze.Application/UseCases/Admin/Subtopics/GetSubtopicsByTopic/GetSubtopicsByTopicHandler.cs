using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Admin.Subtopics.GetSubtopicsByTopic;

public class GetSubtopicsByTopicHandler(ISubtopicRepository subtopicRepository)
    : IRequestHandler<GetSubtopicsByTopicQuery, List<GetSubtopicsByTopicResult>>
{
    public async Task<List<GetSubtopicsByTopicResult>> Handle(GetSubtopicsByTopicQuery query, CancellationToken ct = default)
    {
        IReadOnlyList<Subtopic> subtopics = await subtopicRepository.GetByTopicIdAsync(query.TopicId, ct);

        return [.. subtopics.Select(s => new GetSubtopicsByTopicResult
        {
            Id = s.Id,
            Name = s.SubtopicTranslates.FirstOrDefault()?.Name
        })];
    }
}
