using Common.DAL;
using Common.Domain.Entities;
using Common.Domain.Enums;
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

    public async Task<List<Email>> GetFailedEmailsAsync(int maxRetryCount, EmailPriority priority, CancellationToken cancellationToken = default)
    {
        return await _context.Emails
            .Where(e => e.Priority == priority 
                        && e.Status == EmailStatus.Failed 
                        && e.RetryCount < maxRetryCount)
            .ToListAsync(cancellationToken);
    }
}
