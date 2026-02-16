using PureGaze.Application.Requests;
using PureGaze.Application.UseCases.Content.Templates.TemplatesQuery;

namespace PureGaze.Application.UseCases.Content.Templates.QueryTemplates;

public sealed record GetTemplatesQuery(int Page, int PageSize) : IRequest<GetTemplatesQueryResult>;