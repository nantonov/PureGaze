using Common.Domain.Enums;

namespace Common.Domain.Entities;

public class Notification : BaseEntity<Guid>
{
    public int EmployeeId { get; set; }
    public virtual Employee? Employee { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    
    public NotificationPriority Priority { get; set; }
    public NotificationStatus Status { get; set; }
    

    public DateTime? SentAt { get; set; }
    public string? ErrorMessage { get; set; }
}
