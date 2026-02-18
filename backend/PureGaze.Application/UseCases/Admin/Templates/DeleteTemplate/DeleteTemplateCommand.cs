using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Admin.Templates.DeleteTemplate;

public sealed record DeleteTemplateCommand(int TemplateId) : IRequest;
