using Common.DAL;
using Common.Data.Enums;
using Common.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Notification.Application.Interfaces;

namespace Notification.Infrastructure.Repositories;

public class EmailRepository : IEmailRepository
{
    private readonly AppDbContext _context;

    public EmailRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Email email, CancellationToken cancellationToken = default)
    {
        await _context.Emails.AddAsync(email, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Email email, CancellationToken cancellationToken = default)
    {
        _context.Emails.Update(email);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Email>> GetPendingEmailsAsync(int maxRetryCount, EmailPriority priority, CancellationToken cancellationToken = default)
    {
        return await _context.Emails
            .Where(e => e.Priority == priority 
                        && (e.Status == EmailStatus.InQueue || e.Status == EmailStatus.Failed) 
                        && e.RetryCount < maxRetryCount)
            .OrderBy(e => e.CreatedAt)
            .ToListAsync(cancellationToken);
    }
}
