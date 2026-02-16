using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;
using PureGaze.Application.UseCases.Content.Topics.CreateTopic;
using PureGaze.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace PureGaze.Application.UseCases.Content.Templates.CreateTemplate;

public sealed class CreateTopicHandler(ITopicsRepository topicsRepository)
    : IRequestHandler<CreateTopicCommand, CreateTopicResult>
{
    public async Task<CreateTopicResult> Handle(CreateTopicCommand request, CancellationToken ct)
    {
        if (await topicsRepository.GetByTemplateAsync(request.TemplateId, ct) != null)
            throw new ValidationException($"Topic for template `{request.TemplateId}` already exists");

        var topic = new Topic
        {
            TemplateId = request.TemplateId
        };

        await topicsRepository.AddAsync(topic, ct);
        await topicsRepository.SaveChangesAsync(ct);

        return new CreateTopicResult(topic.Id);
    }
}
