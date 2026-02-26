using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Admin.Templates.GetTemplates;

public sealed record GetTemplatesQuery(int Page, int PageSize) : IRequest<GetTemplatesQueryResult>;