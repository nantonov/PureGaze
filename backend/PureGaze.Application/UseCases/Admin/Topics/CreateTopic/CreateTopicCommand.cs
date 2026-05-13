using PureGaze.Application.Requests;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.UseCases.Admin.Topics.CreateTopic;

public sealed record CreateTopicCommand(
    int TemplateId,
    List<TopicTranslateDto> Translates) : IRequest<CreateTopicResult>;

public sealed record TopicTranslateDto(Language Language, string Name);
