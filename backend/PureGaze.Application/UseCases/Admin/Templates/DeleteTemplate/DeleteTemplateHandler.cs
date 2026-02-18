using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Admin.Templates.DeleteTemplate;

public class DeleteTemplateHandler(ITemplateRepository templateRepository)
    : IRequestHandler<DeleteTemplateCommand>
{
    public async Task Handle(DeleteTemplateCommand request, CancellationToken ct)
    {
        var template = await templateRepository.GetByIdAsync(request.TemplateId, ct)
            ?? throw new KeyNotFoundException($"Template with Id `{request.TemplateId}` was not found");

        templateRepository.Delete(template);

        await templateRepository.SaveChangesAsync(ct);
    }
}
