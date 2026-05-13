using PureGaze.Application.Contracts.Application;
using PureGaze.Application.UseCases.Admin.Topics.EditTopic;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.Extensions;

public static class TopicExtensions
{
    public static TopicDetailsDto ToDetailsDto(this Topic topic)
        => new()
        {
            Id = topic.Id,
            Translates = topic.TopicTranslates.Select(t => new TopicTranslateInfoDto
            {
                Language = t.Language,
                Name = t.Name ?? ""
            }).ToList()
        };

    public static void Update(this Topic topic, IEnumerable<EditTopicTranslateDto> translates)
    {
        foreach (var translateDto in translates)
        {
            topic.TopicTranslates.SyncTranslate(
                translateDto.Language,
                t => t.Name = translateDto.Name,
                lang => new TopicTranslate { TopicId = topic.Id, Language = lang, Name = translateDto.Name },
                t => t.Language == translateDto.Language);
        }
    }
}
