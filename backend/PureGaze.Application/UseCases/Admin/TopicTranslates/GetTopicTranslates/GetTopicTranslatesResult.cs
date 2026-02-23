using PureGaze.Application.Contracts.Application;

namespace PureGaze.Application.UseCases.Admin.TopicTranslates.GetTopicTranslates;

public sealed record GetTopicTranslatesResult(List<TopicTranslateDto> TopicTranslates);
