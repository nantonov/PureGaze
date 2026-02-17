using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Content.Templates.CreateTemplate;

public sealed record CreateTemplateCommand(int CodeId) : IRequest;