using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Content.Topics.DeleteTopic;

public sealed record DeleteTopicsCommand(int Id) : IRequest;

