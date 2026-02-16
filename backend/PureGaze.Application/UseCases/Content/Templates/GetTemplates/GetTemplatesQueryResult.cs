using PureGaze.Application.Contracts.Application;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Content.Templates.TemplatesQuery;

public sealed record GetTemplatesQueryResult(List<TemplateDto> Templates) : IRequest;