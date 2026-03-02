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
        if (await topicsRepository.GetByIdAsync(request.TopicId, ct) == null)
            throw new KeyNotFoundException($"Topic with id `{request.TopicId}` was not found");

        var topicTranslates = await topicTranslatesRepository.GetTopicsTranslatesAsync(request.TopicId);

        if (topicTranslates is not [
            { Language: Language.En } enTopicTranslate,
            { Language: Language.Ru } ruTopicTranslate])
        {
            throw new ValidationException($"Russian and english translates for topic `{request.TopicId}` are not found");
        }

        enTopicTranslate.Name = request.NameEn;
        ruTopicTranslate.Name = request.NameRu;

        await topicTranslatesRepository.SaveChangesAsync();
    }
}
