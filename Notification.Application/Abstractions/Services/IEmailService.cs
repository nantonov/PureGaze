using Common.Data.Enums;
using Common.Domain.Entities;
using Notification.Application.Contracts.Application;

namespace Notification.Application.Abstractions.Services;

public interface IEmailService
{
    Task CreateEmailAsync(CreateEmailRequest dto, CancellationToken cancellationToken = default);
    Task ResendFailedEmailsAsync(CancellationToken cancellationToken = default);
    Task<List<Email>> GetEmailsAsync(int page, int pageSize, EmailStatus status, CancellationToken cancellationToken = default);
}