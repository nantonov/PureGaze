using Common.Data.Enums;
using Common.Domain.Entities;
using Notification.Application.DTOs;

namespace Notification.Application.Services.Interfaces;

public interface INotificationService
{
    Task CreateNotificationAsync(CreateNotificationDto dto, CancellationToken cancellationToken);
    Task<List<Email>> GetFailedEmailsAsync(EmailPriority? priority = null, CancellationToken cancellationToken = default);
}