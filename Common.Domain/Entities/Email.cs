using Common.Domain.Enums;

namespace Common.Domain.Entities;

public class Email : BaseEntity<Guid>
{
    public Guid EmployeeId { get; set; }
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    
    public string To { get; set; } = string.Empty;
    public string From { get; set; } = string.Empty;

    public int RetryCount { get; set; }

    public EmailPriority Priority { get; set; }
    public EmailStatus Status { get; set; }
    

    public DateTime? SentAt { get; set; }
    public string? ErrorMessage { get; set; }
}
