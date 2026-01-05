using Common.DAL;
using Common.Data.Enums;
using Common.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Notification.Application.Abstractions.Infrastructure;

namespace Notification.Infrastructure.Repositories;

public class EmailRepository(AppDbContext context) : IEmailRepository
{
    public ValueTask<Email?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return context.Emails.FindAsync([id], ct);
    }

    public async Task AddAsync(Email email, CancellationToken ct = default) 
        => await context.Emails.AddAsync(email, ct);
    
    public async Task SaveChangesAsync(CancellationToken ct = default)
        => await context.SaveChangesAsync(ct);
    
    public async Task<List<Email>> GetPendingEmailsAsync(CancellationToken ct = default) 
        => await context.Emails
            .Where(e => e.Status == EmailStatus.InQueue || e.Status == EmailStatus.Failed)
            .OrderBy(e => e.CreatedAt)
            .Take(10)
            .ToListAsync(ct);
    
    public async Task<List<Email>> GetEmailsAsync(int page, int pageSize, EmailStatus? status, CancellationToken ct = default)
    {
        var query = context.Emails.AsQueryable();

        if (status.HasValue)
            query = query.Where(e => e.Status == status.Value);

        return await query
            .OrderByDescending(e => e.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);
    }
}
