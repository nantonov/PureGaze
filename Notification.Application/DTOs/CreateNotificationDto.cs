namespace Notification.Application.DTOs;

public class CreateNotificationDto
{
    public Guid EmployeeId { get; set; }
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;

    public DateTime Deadline { get; set; }
}
