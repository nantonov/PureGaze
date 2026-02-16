using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Content.Topics.CreateTopic;

public sealed record CreateTopicCommand(int TemplateId) : IRequest<CreateTopicResult>;
