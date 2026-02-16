using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Content.Topics.CreateTopic;

public sealed class CreateTopicHandler(ITopicsRepository topicsRepository)
    : IRequestHandler<CreateTopicCommand, CreateTopicResult>
{
    public async Task<CreateTopicResult> Handle(CreateTopicCommand request, CancellationToken ct)
    {
        var topic = new Topic
        {
            TemplateId = request.TemplateId
        };

        await topicsRepository.AddAsync(topic, ct);
        await topicsRepository.SaveChangesAsync(ct);

        return new CreateTopicResult(topic.Id);
    }
}
