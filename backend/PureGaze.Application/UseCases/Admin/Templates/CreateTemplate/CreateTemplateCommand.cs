using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Admin.Templates.CreateTemplate;

public sealed record CreateTemplateCommand(int CodeId) : IRequest<CreateTemplateResult>;
