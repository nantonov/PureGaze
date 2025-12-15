namespace Common.Data.Enums;

public enum EmailStatus
{
    InQueue = 1,
    Sending = 2,
    Sent = 3,
    Failed = 4,
    ExceededRetryCount = 5
}