using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Contracts.Application;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Admin.TopicTranslates.GetTopicTranslates;

public sealed class GetTopicTranslatesHandler(
    ITopicsRepository topicsRepository,
    ITopicTranslatesRepository topicTranslatesRepository)
    : IRequestHandler<GetTopicTranslatesQuery, GetTopicTranslatesResult>
{
    public async Task<GetTopicTranslatesResult> Handle(GetTopicTranslatesQuery request, CancellationToken ct)
    {
        if (await topicsRepository.GetByIdAsync(request.TopicId, ct) == null)
            throw new KeyNotFoundException($"Topic with Id `{request.TopicId}` was not found");

        var topicTranslates = await topicTranslatesRepository.GetTopicsTranslatesAsync(
            request.TopicId,
            request.Page,
            request.PageSize,
            ct);

        return new GetTopicTranslatesResult(
            [.. topicTranslates.Select(x => 
                new TopicTranslateDto {TopicId = x.TopicId, Language = x.Language, Name = x.Name})]);
    }
}
