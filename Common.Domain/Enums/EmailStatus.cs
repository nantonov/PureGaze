namespace Common.Domain.Enums;

public enum EmailStatus
{
    InQueue = 1,
    Sending = 2,
    Sent = 3,
    Failed = 4,
    Received = 5,
    SoftBounce = 6,
    HardBounce = 7
}
