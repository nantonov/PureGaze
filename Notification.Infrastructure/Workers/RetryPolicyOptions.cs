namespace Notification.Infrastructure.Workers;

public class RetryPolicyOptions
{
    public int HighPriorityRetryCount { get; set; }
    public int MediumPriorityRetryCount { get; set; }
    public int LowPriorityRetryCount { get; set; }
}
