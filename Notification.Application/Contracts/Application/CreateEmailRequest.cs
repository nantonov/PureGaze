namespace Notification.Application.Contracts.Application;

public class CreateEmailRequest
{
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    public DateTime Deadline { get; set; }
}