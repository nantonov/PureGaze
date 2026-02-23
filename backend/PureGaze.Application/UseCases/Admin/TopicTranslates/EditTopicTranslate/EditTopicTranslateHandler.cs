using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Admin.TopicTranslates.EditTopicTranslate;

public sealed class EditTopicTranslateHandler(
    ITopicsRepository topicsRepository,
    ITopicTranslatesRepository topicTranslatesRepository)
    : IRequestHandler<EditTopicTranslateCommand>
{
    public async Task Handle(EditTopicTranslateCommand request, CancellationToken ct)
    {
        if (await topicsRepository.GetByIdAsync(request.TopicId, ct) == null)
            throw new KeyNotFoundException($"Topic with Id `{request.TopicId}` was not found");

        var topicTranslate = await topicTranslatesRepository.GetByTopicIdAndLanguageAsync(request.TopicId, request.Language, ct) ??
            throw new KeyNotFoundException($"Topic translate for Topic `{request.TopicId}` and language `{request.Language}` doesn't exist");

        topicTranslate.Name = request.Name;

        await topicTranslatesRepository.SaveChangesAsync(ct);
    }
}
