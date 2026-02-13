using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Content.Templates.DeleteTemplate;

public class DeleteTemplateHandler(ITemplateRepository templateRepository)
    : IRequestHandler<DeleteTemplateCommand>
{
    public async Task Handle(DeleteTemplateCommand request, CancellationToken ct)
    {
        var template = await templateRepository.GetByCodeIdAsync(request.CodeId, ct)
            ?? throw new KeyNotFoundException($"Template with code `{request.CodeId}` was not found");
        
        templateRepository.Delete(template);
        
        await templateRepository.SaveChangesAsync(ct);
    }
}
