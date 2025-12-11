using Common.Domain.Enums;

namespace Common.Domain.Entities;

public class Email : BaseEntity<Guid>
{
    public int EmployeeId { get; set; }
    public virtual Employee? Employee { get; set; }

    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    
    public string To { get; set; } = string.Empty;
    public string From { get; set; } = string.Empty;
    public string? Cc { get; set; }
    public string? Bcc { get; set; }

    public EmailPriority Priority { get; set; }
    public EmailStatus Status { get; set; }
    

    public DateTime? SentAt { get; set; }
    public string? ErrorMessage { get; set; }
}
