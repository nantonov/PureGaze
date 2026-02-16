using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Content.Topics.CreateTopic;

public sealed class CreateTopicHandler(
    ITemplateRepository templateRepository,
    ITopicsRepository topicsRepository)
    : IRequestHandler<CreateTopicCommand, CreateTopicResult>
{
    public async Task<CreateTopicResult> Handle(CreateTopicCommand request, CancellationToken ct)
    {
        if (await templateRepository.GetByIdAsync(request.TemplateId, ct) == null)
            throw new KeyNotFoundException($"Template with Id `{request.TemplateId}` was not found");

        var topic = new Topic
        {
            TemplateId = request.TemplateId
        };

        await topicsRepository.AddAsync(topic, ct);
        await topicsRepository.SaveChangesAsync(ct);

        return new CreateTopicResult(topic.Id);
    }
}
