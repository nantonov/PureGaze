using Common.Data.Enums;
using Common.Domain.Entities;
using Notification.Application.Contracts.Application;

namespace Notification.Application.Abstractions.Services;

public interface IEmailService
{
    Task CreateEmailAsync(CreateEmailRequest dto, CancellationToken cancellationToken = default);
    Task<List<Email>> GetFailedEmailsAsync(CancellationToken cancellationToken = default);
}