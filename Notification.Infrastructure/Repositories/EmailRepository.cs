using Common.DAL;
using Common.Data.Enums;
using Common.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Notification.Application.Contracts;

namespace Notification.Infrastructure.Repositories;

public class EmailRepository(AppDbContext context) : IEmailRepository
{
    public async Task AddAsync(Email email, CancellationToken ct = default)
    {
        await context.Emails.AddAsync(email, ct);
    }
    
    public async Task SaveChangesAsync(CancellationToken ct = default)
        => await context.SaveChangesAsync(ct);
    
    public async Task<List<Email>> GetFailedEmailsReadOnlyAsync(
        EmailPriority? priority = null,
        CancellationToken ct = default)
    {
        var query = context.Emails
            .Where(e => e.Status == EmailStatus.ExceededRetryCount);

        if (priority.HasValue)
        {
            query = query.Where(e => e.Priority == priority.Value);
        }

        return await query.AsNoTracking().ToListAsync(ct);
    }

    public async Task<List<Email>> GetPendingEmailsAsync(int maxRetryCount, EmailPriority priority, CancellationToken ct = default)
    {
        return await context.Emails
            .Where(e => e.Priority == priority 
                        && (e.Status == EmailStatus.InQueue || e.Status == EmailStatus.Failed) 
                        && e.RetryCount < maxRetryCount)
            .OrderBy(e => e.CreatedAt)
            .ToListAsync(ct);
    }
}
