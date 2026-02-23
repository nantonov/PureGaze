using PureGaze.Application.Contracts.Application;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Admin.TopicTranslates.GetTopicTranslates;

public sealed record GetTopicTranslatesResult(List<TopicTranslateDto> TopicTranslates) : IRequest;
