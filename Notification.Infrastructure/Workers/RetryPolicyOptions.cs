namespace Notification.Infrastructure.Workers;

public class RetryPolicyOptions
{
    public static string SectionName = "RetryPolicy";

    public int MaxRetryCount { get; set; } = 3;
    public int DelayInSeconds { get; set; } = 30;
}
