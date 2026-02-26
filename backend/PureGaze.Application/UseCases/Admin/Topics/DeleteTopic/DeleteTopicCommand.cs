using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Admin.Topics.DeleteTopic;

public sealed record DeleteTopicCommand(int Id) : IRequest;

