using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Content.Topics.CreateTopic;

public sealed record CreateTopicResult(int TopicId) : IRequest;