using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.UseCases.Admin.Topics.CreateTopic;

public sealed class CreateTopicHandler(
    ITemplateRepository templateRepository,
    ITopicsRepository topicsRepository,
    ITopicTranslatesRepository topicTranslatesRepository)
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

        await topicTranslatesRepository.AddAsync(new TopicTranslate
        {
            TopicId = topic.Id,
            Language = Language.Ru,
            Name = request.NameRu
        }, ct);

        await topicTranslatesRepository.AddAsync(new TopicTranslate
        {
            TopicId = topic.Id,
            Language = Language.En,
            Name = request.NameEn
        }, ct);

        await topicTranslatesRepository.SaveChangesAsync();

        return new CreateTopicResult(topic.Id);
    }
}
