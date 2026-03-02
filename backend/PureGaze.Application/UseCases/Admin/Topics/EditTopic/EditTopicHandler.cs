using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;
using PureGaze.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PureGaze.Application.UseCases.Admin.Topics.EditTopic;

public sealed class EditTopicHandler(
    ITopicsRepository topicsRepository,
    ITopicTranslatesRepository topicTranslatesRepository
    ) : IRequestHandler<EditTopicCommand>
{
    public async Task Handle(EditTopicCommand request, CancellationToken ct)
    {
        var topic = await topicsRepository.GetByIdAsync(request.TopicId, ct);
        if (topic == null)
            throw new KeyNotFoundException($"Topic with id `{request.TopicId}` was not found");

        var enTopicTranslate = topic.TopicTranslates.FirstOrDefault(x => x.Language == Language.En);
        var ruTopicTranslate = topic.TopicTranslates.FirstOrDefault(x => x.Language == Language.Ru);
        if (enTopicTranslate == null || ruTopicTranslate == null)
        {
            throw new ValidationException($"Russian and english translates for topic `{request.TopicId}` are not found");
        }

        enTopicTranslate.Name = request.NameEn;
        ruTopicTranslate.Name = request.NameRu;

        await topicTranslatesRepository.SaveChangesAsync();
    }
}
