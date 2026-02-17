using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Content.Topics.GetTopicsForTemplate;

public sealed record GetTopicsForTemplateQuery(
    int TemplateId,
    int Page,
    int PageSize) : IRequest<GetTopicsForTemplateResult>;
