using Common.Domain.Entities;
using Common.Domain.Enums;
using Notification.Application.DTOs;
using Notification.Application.Interfaces;
using Notification.Application.Strategies;

namespace Notification.Application.Services;

public class NotificationService(
    IEmailRepository emailRepository,
    IEmailSender emailSender,
    INotificationStrategy standardPriorityStrategy,
    INotificationStrategy highPriorityStrategy)
{
    public async Task CreateNotificationAsync(CreateNotificationDto dto, CancellationToken cancellationToken = default)
    {
        var email = new Email
        {
            Id = Guid.NewGuid(),
            EmployeeId = dto.EmployeeId,
            Subject = dto.Subject,
            Body = dto.Body,
            To = dto.To,
            From = "system@example.com",
            RetryCount = 0,
            Status = EmailStatus.InQueue
        };

        var timeToDeadline = dto.Deadline - DateTime.UtcNow;
        
        email.Priority = timeToDeadline switch
        {
            _ when timeToDeadline <= TimeSpan.FromHours(3) 
                => EmailPriority.High,
            _ when timeToDeadline <= TimeSpan.FromHours(24)
                => EmailPriority.Normal,
            _ 
                => EmailPriority.Low
        };

        var strategy = email.Priority == EmailPriority.High 
            ? highPriorityStrategy 
            : standardPriorityStrategy;

        await strategy.ProcessAsync(email, emailRepository, emailSender, cancellationToken);
    }
}
