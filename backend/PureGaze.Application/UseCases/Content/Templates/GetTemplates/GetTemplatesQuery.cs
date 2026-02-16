using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Content.Templates.GetTemplates;

public sealed record GetTemplatesQuery(int Page, int PageSize) : IRequest<GetTemplatesQueryResult>;