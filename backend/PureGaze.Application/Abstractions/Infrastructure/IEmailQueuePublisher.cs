namespace PureGaze.Application.Abstractions.Infrastructure;

public interface IEmailQueuePublisher
{
    Task PublishAsync(Guid emailId, CancellationToken ct = default);
}
