using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Contracts.Application;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Content.Topics.GetTopicForTemplate;

public class GetTopicForTemplateHandler(
    ITemplateRepository templateRepository,
    ITopicsRepository topicsRepository)
    : IRequestHandler<GetTopicForTemplateQuery, GetTopicForTemplateResult>
{
    public async Task<GetTopicForTemplateResult> Handle(GetTopicForTemplateQuery request, CancellationToken ct)
    {
        var template = await templateRepository.GetByIdAsync(request.TemplateId, ct)
            ?? throw new KeyNotFoundException($"Template with Id `{request.TemplateId}` was not found");

        var topic = await topicsRepository.GetByTemplateAsync(request.TemplateId, ct);

        if (topic == null)
        {
            return new GetTopicForTemplateResult(null);
        }

        return new GetTopicForTemplateResult(new TopicDto(topic.Id));
    }
}
