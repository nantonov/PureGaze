using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Admin.Topics.EditTopic;

public sealed class EditTopicHandler(ITopicsRepository topicsRepository) : IRequestHandler<EditTopicCommand>
{
    public async Task Handle(EditTopicCommand request, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(request.Translates);

        if (request.Translates.Count == 0)
            throw new ArgumentException("At least one topic translate is required.");

        Topic topic = await topicsRepository.GetByIdAsync(request.TopicId, ct)
            ?? throw new KeyNotFoundException($"Topic with id `{request.TopicId}` was not found");

        EditTopicCommand.Update(topic, request);

        await topicsRepository.SaveChangesAsync(ct);
    }
}
