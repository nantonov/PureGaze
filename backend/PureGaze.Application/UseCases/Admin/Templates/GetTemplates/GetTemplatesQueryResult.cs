using PureGaze.Application.Contracts.Application;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Admin.Templates.GetTemplates;

public sealed record GetTemplatesQueryResult(List<TemplateDto> Templates) : IRequest;