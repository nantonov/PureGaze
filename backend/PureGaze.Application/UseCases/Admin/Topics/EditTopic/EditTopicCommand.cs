using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.UseCases.Admin.Topics.EditTopic;

public sealed record EditTopicCommand(
    int TopicId,
    List<EditTopicTranslateDto> Translates) : IRequest
{
    public static void Update(Topic topic, EditTopicCommand command)
    {
        foreach (EditTopicTranslateDto translate in command.Translates)
        {
            TranslationSync.Update(
                topic.TopicTranslates,
                translate.Language,
                current => current.Name = translate.Name,
                language => new TopicTranslate
                {
                    TopicId = topic.Id,
                    Language = language,
                    Name = translate.Name
                },
                current => current.Language == translate.Language);
        }
    }
}

public sealed record EditTopicTranslateDto(Language Language, string Name);
