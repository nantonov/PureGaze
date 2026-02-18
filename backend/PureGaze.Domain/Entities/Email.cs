using PureGaze.Domain.Enums;

namespace PureGaze.Domain.Entities;

public class Email : BaseEntity<Guid>
{
    public string? From { get; set; }
    public string? To { get; set; }
    public string? Subject { get; set; }
    public string? Body { get; set; }
    public int RetryCount { get; set; }
    public EmailStatus Status { get; set; }
    public DateTime? SentAt { get; set; }
}
