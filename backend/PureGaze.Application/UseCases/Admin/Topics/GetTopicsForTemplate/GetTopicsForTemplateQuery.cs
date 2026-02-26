using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Admin.Topics.GetTopicsForTemplate;

public sealed record GetTopicsForTemplateQuery(
    int TemplateId,
    int Page,
    int PageSize) : IRequest<GetTopicsForTemplateResult>;
