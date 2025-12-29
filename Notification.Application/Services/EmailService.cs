using Common.Data.Enums;
using Common.Domain.Entities;
using Notification.Application.Abstractions.Infrastructure;
using Notification.Application.Abstractions.Services;
using Notification.Application.Contracts.Application;

namespace Notification.Application.Services;

public class EmailService(IEmailRepository emailRepository) 
    : IEmailService
{
    public async Task CreateEmailAsync(CreateEmailRequest dto, CancellationToken cancellationToken = default)
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
        
        await emailRepository.AddAsync(email, cancellationToken);
        await emailRepository.SaveChangesAsync(cancellationToken);
    }

    public Task<List<Email>> GetFailedEmailsAsync(CancellationToken cancellationToken = default) 
        => emailRepository.GetFailedEmailsReadOnlyAsync(cancellationToken);
}
