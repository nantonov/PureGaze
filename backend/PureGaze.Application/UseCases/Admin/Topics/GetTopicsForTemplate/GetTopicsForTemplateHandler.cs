using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Contracts.Application;
using PureGaze.Application.Requests;

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

        var topics =
            await topicsRepository.GetTopicsByTemplateIdAsync(request.TemplateId, request.Page, request.PageSize, ct);

        return
            new GetTopicsForTemplateResult(
                [.. topics.Select(topic => new TopicDto { Id = topic.Id, Name = topic.TopicTranslates.FirstOrDefault()?.Name })]);
    }
}