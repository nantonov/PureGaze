using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Admin.TopicTranslates.DeleteTopicTranslate;

public sealed class DeleteTopicTranslateHandler(
    ITopicsRepository topicsRepository,
    ITopicTranslatesRepository topicTranslatesRepository)
    : IRequestHandler<DeleteTopicTranslateCommand>
{
    public async Task Handle(DeleteTopicTranslateCommand request, CancellationToken ct)
    {
        if (await topicsRepository.GetByIdAsync(request.TopicId, ct) == null)
            throw new KeyNotFoundException($"Topic with Id `{request.TopicId}` was not found");

        var topicTranslate = await topicTranslatesRepository.GetByTopicIdAndLanguageAsync(request.TopicId, request.Language, ct) ??
            throw new KeyNotFoundException($"Topic translate for Topic `{request.TopicId}` and language `{request.Language}` doesn't exist");

        topicTranslatesRepository.Delete(topicTranslate);
        await topicTranslatesRepository.SaveChangesAsync(ct);
    }
}
