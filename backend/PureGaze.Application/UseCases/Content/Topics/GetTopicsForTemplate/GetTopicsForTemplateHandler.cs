using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Contracts.Application;
using PureGaze.Application.Requests;
using System.ComponentModel.DataAnnotations;

namespace PureGaze.Application.UseCases.Content.Topics.GetTopicsForTemplate;

public class GetTopicsForTemplateHandler(
    ITemplateRepository templateRepository,
    ITopicsRepository topicsRepository)
    : IRequestHandler<GetTopicsForTemplateQuery, GetTopicsForTemplateResult>
{
    public async Task<GetTopicsForTemplateResult> Handle(GetTopicsForTemplateQuery request, CancellationToken ct)
    {
        if (await templateRepository.GetByIdAsync(request.TemplateId, ct) == null)
            throw new KeyNotFoundException($"Template with Id `{request.TemplateId}` was not found");
        if (request.Page < 1)
            throw new ValidationException("Page cannot be < 1");
        if (request.PageSize < 1)
            throw new ValidationException("PageSize cannot be < 1");

        var topics = await topicsRepository
            .QueryByTemplateAsync(request.TemplateId, request.Page, request.PageSize)
            .Select(x => new TopicDto(x.Id))
            .ToListAsync(ct);

        return new GetTopicsForTemplateResult(topics);
    }
}
