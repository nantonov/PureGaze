using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace PureGaze.Application.UseCases.Admin.Templates.CreateTemplate;

public class CreateTemplateHandler(ITemplateRepository templateRepository)
    : IRequestHandler<CreateTemplateCommand, CreateTemplateResult>
{
    public async Task<CreateTemplateResult> Handle(CreateTemplateCommand request, CancellationToken ct)
    {
        if (await templateRepository.GetByCodeIdAsync(request.CodeId, ct) != null)
            throw new ValidationException($"Template with code `{request.CodeId}` already exists");

        var template = new Template
        {
            CodeId = request.CodeId
        };

        await templateRepository.AddAsync(template, ct);
        await templateRepository.SaveChangesAsync(ct);

        return new CreateTemplateResult(template.Id);
    }
}
