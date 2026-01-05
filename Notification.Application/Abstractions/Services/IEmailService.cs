using Common.Data.Enums;
using Common.Domain.Entities;
using Notification.Application.Contracts.Application;

namespace Notification.Application.Abstractions.Services;

public interface IEmailService
{
    Task CreateEmailAsync(CreateEmailRequest request, CancellationToken ct = default);
    Task ResendEmailManuallyAsync(Guid id, CancellationToken ct = default);
    Task<List<Email>> GetEmailsAsync(int page, int pageSize, EmailStatus status, CancellationToken ct = default);
}