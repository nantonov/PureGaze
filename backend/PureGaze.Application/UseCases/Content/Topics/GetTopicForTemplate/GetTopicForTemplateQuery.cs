using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Content.Topics.GetTopicForTemplate;

public sealed record GetTopicForTemplateQuery(int TemplateId) : IRequest<GetTopicForTemplateResult>;
