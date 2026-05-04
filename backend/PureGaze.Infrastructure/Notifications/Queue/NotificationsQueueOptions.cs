namespace PureGaze.Infrastructure.Notifications.Queue;

public class NotificationsQueueOptions
{
    public const string SectionName = "NotificationsQueue";
    public const string QueueName = "email-queue";

    public string ConnectionString { get; set; } = string.Empty;
}
