using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Admin.Templates.GetTemplates;

public class GetTemplatesHandler(ITemplateRepository templateRepository)
    : IRequestHandler<GetTemplatesQuery, GetTemplatesQueryResult>
{
    public async Task<GetTemplatesQueryResult> Handle(GetTemplatesQuery request, CancellationToken ct)
    {
        IReadOnlyList<Template> templates =
            await templateRepository.GetTemplates(request.Page, request.PageSize, ct);

        return
            new GetTemplatesQueryResult(
                [.. templates.Select(GetTemplateDto.ToDto)]);
    }
}
