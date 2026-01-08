using Assessment.Application.Abstractions.Infrastructure;
using Common.DAL;
using Common.Domain.Entities;

namespace Assessment.Infrastructure.Repositories;

public class EmailRepository(AppDbContext context) : IEmailRepository
{
    public async Task AddAsync(Email email, CancellationToken ct = default) 
        => await context.Emails.AddAsync(email, ct);
    
    public async Task SaveChangesAsync(CancellationToken ct = default)
        => await context.SaveChangesAsync(ct);
}
