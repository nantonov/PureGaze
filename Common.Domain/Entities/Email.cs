using Common.Data.Enums;

namespace Common.Domain.Entities;

public class Email : BaseEntity<Guid>
{
    public string From { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;

    public int RetryCount { get; set; }
    public EmailStatus Status { get; set; }
    
    public DateTime? SentAt { get; set; }
}
