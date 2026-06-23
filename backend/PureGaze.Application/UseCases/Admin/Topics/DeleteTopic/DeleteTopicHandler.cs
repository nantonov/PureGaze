using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Admin.Topics.DeleteTopic;

public sealed class DeleteTopicHandler(
    ITopicsRepository topicsRepository)
    : IRequestHandler<DeleteTopicCommand>
{
    public async Task Handle(DeleteTopicCommand request, CancellationToken ct)
    {
        Topic? topic = await topicsRepository.GetByIdAsync(request.Id, ct);
        if (topic == null)
            throw new KeyNotFoundException($"Topic with id `{request.Id}` was not found");

        topicsRepository.Delete(topic);

        await topicsRepository.SaveChangesAsync(ct);
    }
}
