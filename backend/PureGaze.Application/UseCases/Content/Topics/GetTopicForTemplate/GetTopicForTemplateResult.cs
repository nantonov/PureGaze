using PureGaze.Application.Contracts.Application;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Content.Topics.GetTopics;

public sealed record GetTopicForTemplateResult(TopicDto? Topic) : IRequest;
