using Common.DAL;
using Common.Data.Enums;
using Common.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Notification.Application.Abstractions.Infrastructure;

namespace Notification.Infrastructure.Repositories;

public class EmailRepository(AppDbContext context) : IEmailRepository
{
    public async Task AddAsync(Email email, CancellationToken ct = default) 
        => await context.Emails.AddAsync(email, ct);
    
    public async Task SaveChangesAsync(CancellationToken ct = default)
        => await context.SaveChangesAsync(ct);
    
    public async Task<List<Email>> GetFailedEmailsReadOnlyAsync(CancellationToken ct = default) 
        => await context.Emails 
            .Where(e => e.Status == EmailStatus.ExceededRetryCount)
            .AsNoTracking()
            .ToListAsync(ct);
    
    public async Task<List<Email>> GetPendingEmailsAsync(CancellationToken ct = default) 
        => await context.Emails
            .Where(e => e.Status == EmailStatus.InQueue || e.Status == EmailStatus.Failed)
            .OrderBy(e => e.CreatedAt)
            .Take(10)
            .ToListAsync(ct);
}
