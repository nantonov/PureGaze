using PureGaze.Application.Requests;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.UseCases.Admin.Topics.EditTopic;

public sealed record EditTopicCommand(
    int TopicId,
    List<EditTopicTranslateDto> Translates) : IRequest;

public sealed record EditTopicTranslateDto(Language Language, string Name);
