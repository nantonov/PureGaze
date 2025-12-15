using Common.Domain.Entities;
using Common.Domain.Enums;
using Notification.Application.DTOs;
using Notification.Application.Interfaces;
using Notification.Application.Strategies;

namespace Notification.Application.Services;

public class NotificationService
{
    private readonly IEmailRepository _emailRepository;
    private readonly IEmailSender _emailSender;
    
    private readonly INotificationStrategy _highPriorityStrategy;
    private readonly INotificationStrategy _standardPriorityStrategy;

    public NotificationService(IEmailRepository emailRepository, IEmailSender emailSender, INotificationStrategy standardPriorityStrategy, INotificationStrategy highPriorityStrategy)
    {
        _emailRepository = emailRepository;
        _emailSender = emailSender;
        _standardPriorityStrategy = standardPriorityStrategy;
        _highPriorityStrategy = highPriorityStrategy;
    }

    public async Task CreateNotificationAsync(CreateNotificationDto dto, CancellationToken cancellationToken = default)
    {
        var email = new Email
        {
            Id = Guid.NewGuid(),
            EmployeeId = dto.EmployeeId,
            Subject = dto.Subject,
            Body = dto.Body,
            To = dto.To,
            From = "system@example.com", // Default sender
            RetryCount = 0,
            Status = EmailStatus.InQueue // Default, will be updated by strategy
        };

        // Priority Logic
        var timeToDeadline = dto.Deadline - DateTime.UtcNow;
        if (timeToDeadline <= TimeSpan.FromHours(3))
        {
            email.Priority = EmailPriority.High;
        }
        else if (timeToDeadline <= TimeSpan.FromHours(24))
        {
            email.Priority = EmailPriority.Normal;
        }
        else
        {
            email.Priority = EmailPriority.Low;
        }

        // Strategy Selection
        INotificationStrategy strategy = email.Priority == EmailPriority.High 
            ? _highPriorityStrategy 
            : _standardPriorityStrategy;

        await strategy.ProcessAsync(email, _emailRepository, _emailSender, cancellationToken);
    }
}
