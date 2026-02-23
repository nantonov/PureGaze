using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Admin.TopicTranslates.CreateTopicTranslate;

public sealed class CreateTopicTranslateHandler(
    ITopicsRepository topicsRepository,
    ITopicTranslatesRepository topicTranslatesRepository)
    : IRequestHandler<CreateTopicTranslateCommand>
{
    public async Task Handle(CreateTopicTranslateCommand request, CancellationToken ct)
    {
        if (await topicsRepository.GetByIdAsync(request.TopicId, ct) == null)
            throw new KeyNotFoundException($"Topic with Id `{request.TopicId}` was not found");

        if (await topicTranslatesRepository.GetByTopicIdAndLanguageAsync(request.TopicId, request.Language, ct) != null)
            throw new KeyNotFoundException($"Topic translate for Topic `{request.TopicId}` and language `{request.Language}` already exists");

        var topicTranslate = new TopicTranslate
        {
            TopicId = request.TopicId,
            Language = request.Language,
            Name = request.Name,
        };

        await topicTranslatesRepository.AddAsync(topicTranslate, ct);
        await topicTranslatesRepository.SaveChangesAsync(ct);
    }
}
