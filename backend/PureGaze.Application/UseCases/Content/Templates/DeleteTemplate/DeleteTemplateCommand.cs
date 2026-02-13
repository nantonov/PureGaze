using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Content.Templates.DeleteTemplate;

public sealed record DeleteTemplateCommand(int CodeId) : IRequest;
