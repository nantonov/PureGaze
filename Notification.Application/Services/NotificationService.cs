using Common.Data.Enums;
using Common.Domain.Entities;
using Notification.Application.Contracts;
using Notification.Application.DTOs;
using Notification.Application.Services.Interfaces;

namespace Notification.Application.Services;

public class NotificationService(IEmailRepository emailRepository) : INotificationService
{
    public async Task CreateNotificationAsync(CreateNotificationDto dto, CancellationToken cancellationToken = default)
    {
        var email = new Email
        {
            Id = Guid.NewGuid(),
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

        await emailRepository.AddAsync(email, cancellationToken);
    }

    public Task<List<Email>> GetFailedEmailsAsync(EmailPriority? priority = null,
        CancellationToken cancellationToken = default) =>
        emailRepository.GetFailedEmailsAsync(priority, cancellationToken);
}
