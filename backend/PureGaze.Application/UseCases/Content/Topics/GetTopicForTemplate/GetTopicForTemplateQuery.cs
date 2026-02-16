using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Content.Topics.GetTopics;

public sealed record GetTopicForTemplateQuery(int TemplateId) : IRequest<GetTopicForTemplateResult>;
