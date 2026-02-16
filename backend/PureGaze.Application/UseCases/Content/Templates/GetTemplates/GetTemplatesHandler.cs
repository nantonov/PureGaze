using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Contracts.Application;
using PureGaze.Application.Requests;
using PureGaze.Application.UseCases.Content.Templates.QueryTemplates;
using PureGaze.Application.UseCases.Content.Templates.TemplatesQuery;
using System.ComponentModel.DataAnnotations;

namespace PureGaze.Application.UseCases.Content.Templates.GetTemplates;

public class GetTemplatesHandler(ITemplateRepository templateRepository)
    : IRequestHandler<GetTemplatesQuery, GetTemplatesQueryResult>
{
    public async Task<GetTemplatesQueryResult> Handle(GetTemplatesQuery request, CancellationToken ct)
    {
        if (request.Page < 1)
            throw new ValidationException("Page cannot be < 1");
        if (request.PageSize < 1)
            throw new ValidationException("PageSize cannot be < 1");

        var templates = await templateRepository.Query(request.Page, request.PageSize, ct)
            .Select(x => new TemplateDto(x.Id))
            .ToListAsync();

        return new GetTemplatesQueryResult(templates);
    }
}
