using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;
using System.ComponentModel.DataAnnotations;

namespace PureGaze.Application.UseCases.Content.Templates.CreateTemplate;

public class CreateTemplateHandler(ITemplateRepository templateRepository)
    : IRequestHandler<CreateTemplateCommand>
{
    public async Task Handle(CreateTemplateCommand request, CancellationToken ct)
    {
        if (await templateRepository.GetByCodeIdAsync(request.CodeId) != null)
            throw new ValidationException($"Template with code `{request.CodeId}` already exists");

        await templateRepository.AddAsync(new() { CodeId = request.CodeId });
        await templateRepository.SaveChangesAsync();
    }
}
