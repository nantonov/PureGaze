using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Content.Topics.DeleteTopic;

public sealed class DeleteTopicHandler(ITopicsRepository topicsRepository)
    : IRequestHandler<DeleteTopicsCommand>
{
    public async Task Handle(DeleteTopicsCommand request, CancellationToken ct)
    {
        var topic = await topicsRepository.GetByIdAsync(request.Id, ct);
        if (topic == null)
            throw new KeyNotFoundException($"Topic with id `{request.Id}` was not found");

        topicsRepository.Delete(topic);
        await topicsRepository.SaveChangesAsync(ct);
    }
}
