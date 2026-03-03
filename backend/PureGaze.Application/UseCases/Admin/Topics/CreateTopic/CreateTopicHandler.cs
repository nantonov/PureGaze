using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.UseCases.Admin.Topics.CreateTopic;

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
            TemplateId = request.TemplateId,
        };

        topic.TopicTranslates.Add(new TopicTranslate
        {
            Language = Language.Ru,
            Name = request.NameRu
        });

        topic.TopicTranslates.Add(new TopicTranslate
        {
            Language = Language.En,
            Name = request.NameEn
        });

        await topicsRepository.AddAsync(topic, ct);
        await topicsRepository.SaveChangesAsync(ct);

        return new CreateTopicResult {TopicId = topic.Id};
    }
}
