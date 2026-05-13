using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Contracts.Application;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Admin.Subtopics.GetSubtopicsByTopic;

public class GetSubtopicsByTopicHandler(ISubtopicRepository subtopicRepository)
    : IRequestHandler<GetSubtopicsByTopicQuery, List<SubtopicListItemDto>>
{
    public async Task<List<SubtopicListItemDto>> Handle(GetSubtopicsByTopicQuery query, CancellationToken ct = default)
    {
        var subtopics = await subtopicRepository.GetByTopicIdAsync(query.TopicId, ct);

        return subtopics.Select(s => new SubtopicListItemDto
        {
            Id = s.Id,
            Name = s.SubtopicTranslates.FirstOrDefault()?.Name
        }).ToList();
    }
}
