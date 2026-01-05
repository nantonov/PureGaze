using Common.DAL;
using Common.Domain.Entities;
using Notification.Application.Abstractions.Infrastructure;

namespace Notification.Infrastructure.Repositories;

public class EmailRepository(AppDbContext context) : IEmailRepository
{
    public async Task AddAsync(Email email, CancellationToken ct = default) 
        => await context.Emails.AddAsync(email, ct);
    
    public async Task SaveChangesAsync(CancellationToken ct = default)
        => await context.SaveChangesAsync(ct);
}
