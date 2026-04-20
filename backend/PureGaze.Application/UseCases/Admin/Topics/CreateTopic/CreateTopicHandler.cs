using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Admin.Topics.CreateTopic;

public sealed class CreateTopicHandler(
    ITemplateRepository templateRepository,
    ITopicsRepository topicsRepository)
    : IRequestHandler<CreateTopicCommand, CreateTopicResult>
{
    public async Task<CreateTopicResult> Handle(CreateTopicCommand request, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(request.Translates);

        if (request.Translates.Count == 0)
            throw new ArgumentException("At least one topic translate is required.");

        if (await templateRepository.GetByIdAsync(request.TemplateId, ct) == null)
            throw new KeyNotFoundException($"Template with Id `{request.TemplateId}` was not found");

        var topic = new Topic { TemplateId = request.TemplateId };

        foreach (var t in request.Translates)
            topic.TopicTranslates.Add(new TopicTranslate { Language = t.Language, Name = t.Name });

        await topicsRepository.AddAsync(topic, ct);
        await topicsRepository.SaveChangesAsync(ct);

        return new CreateTopicResult { TopicId = topic.Id };
    }
}
