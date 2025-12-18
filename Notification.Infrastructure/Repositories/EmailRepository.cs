using Common.DAL;
using Common.Data.Enums;
using Common.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Notification.Application.Contracts;

namespace Notification.Infrastructure.Repositories;

public class EmailRepository(AppDbContext context) : IEmailRepository
{
    public async Task AddAsync(Email email, CancellationToken cancellationToken = default)
    {
        await context.Emails.AddAsync(email, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Email email, CancellationToken cancellationToken = default)
    {
        context.Emails.Update(email);
        await context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<List<Email>> GetFailedEmailsAsync(
        EmailPriority? priority = null,
        CancellationToken cancellationToken = default)
    {
        var query = context.Emails
            .Where(e => e.Status == EmailStatus.ExceededRetryCount);

        if (priority.HasValue)
        {
            query = query.Where(e => e.Priority == priority.Value);
        }

        return await query.ToListAsync(cancellationToken);
    }


    public async Task<List<Email>> GetPendingEmailsAsync(int maxRetryCount, EmailPriority priority, CancellationToken cancellationToken = default)
    {
        var a = await context.Emails
            .Where(e => e.Priority == priority 
                        && (e.Status == EmailStatus.InQueue || e.Status == EmailStatus.Failed) 
                        && e.RetryCount < maxRetryCount)
            .OrderBy(e => e.CreatedAt)
            .ToListAsync(cancellationToken);

        return a;
    }
}
