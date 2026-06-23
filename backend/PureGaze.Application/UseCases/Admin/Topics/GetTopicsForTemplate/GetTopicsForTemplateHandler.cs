using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Admin.Topics.GetTopicsForTemplate;

public class GetTopicsForTemplateHandler(
    ITemplateRepository templateRepository,
    ITopicsRepository topicsRepository)
    : IRequestHandler<GetTopicsForTemplateQuery, GetTopicsForTemplateResult>
{
    public async Task<GetTopicsForTemplateResult> Handle(GetTopicsForTemplateQuery request, CancellationToken ct)
    {
        if (await templateRepository.GetByIdAsync(request.TemplateId, ct) == null)
            throw new KeyNotFoundException($"Template with Id `{request.TemplateId}` was not found");

        IReadOnlyList<Topic> topics =
            await topicsRepository.GetTopicsByTemplateIdAsync(request.TemplateId, request.Page, request.PageSize, ct);

        return
            new GetTopicsForTemplateResult([.. topics.Select(GetTopicsForTemplateDto.ToDto)]);
    }
}
