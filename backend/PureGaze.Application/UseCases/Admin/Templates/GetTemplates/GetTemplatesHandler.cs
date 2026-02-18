using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;
using PureGaze.Application.Contracts.Application;

namespace PureGaze.Application.UseCases.Admin.Templates.GetTemplates;

public class GetTemplatesHandler(ITemplateRepository templateRepository)
    : IRequestHandler<GetTemplatesQuery, GetTemplatesQueryResult>
{
    public async Task<GetTemplatesQueryResult> Handle(GetTemplatesQuery request, CancellationToken ct)
    {
        var templates = 
            await templateRepository.GetTemplates(request.Page, request.PageSize, ct);

        return 
            new GetTemplatesQueryResult(
                [..templates.Select(template => new TemplateDto { Id = template.Id, CodeName = template.Code?.Display })]);
    }
}
