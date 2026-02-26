using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Admin.Topics.CreateTopic;

public sealed record CreateTopicResult(int TopicId) : IRequest;