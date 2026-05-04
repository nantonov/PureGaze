using Azure.Storage.Queues;
using Microsoft.Extensions.Options;
using PureGaze.Application.Abstractions.Infrastructure;

namespace PureGaze.Infrastructure.Notifications.Queue;

public class EmailQueuePublisher(IOptions<NotificationsQueueOptions> options) : IEmailQueuePublisher
{
    private readonly QueueClient _queue = new(
        options.Value.ConnectionString,
        NotificationsQueueOptions.QueueName,
        new QueueClientOptions { MessageEncoding = QueueMessageEncoding.Base64 });

    public async Task PublishAsync(Guid emailId, CancellationToken ct = default)
    {
        await _queue.CreateIfNotExistsAsync(cancellationToken: ct);
        await _queue.SendMessageAsync(emailId.ToString(), ct);
    }
}