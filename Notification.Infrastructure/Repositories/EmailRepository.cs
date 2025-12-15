using Common.DAL;
using Common.Domain.Entities;
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
}
