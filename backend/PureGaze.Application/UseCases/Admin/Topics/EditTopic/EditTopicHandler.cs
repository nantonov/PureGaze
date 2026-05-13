using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Extensions;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Admin.Topics.EditTopic;

public sealed class EditTopicHandler(ITopicsRepository topicsRepository) : IRequestHandler<EditTopicCommand>
{
    public async Task Handle(EditTopicCommand request, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(request.Translates);

        if (request.Translates.Count == 0)
            throw new ArgumentException("At least one topic translate is required.");

        var topic = await topicsRepository.GetByIdAsync(request.TopicId, ct)
            ?? throw new KeyNotFoundException($"Topic with id `{request.TopicId}` was not found");

        topic.Update(request.Translates);

        await topicsRepository.SaveChangesAsync(ct);
    }
}
