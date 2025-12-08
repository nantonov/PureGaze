using Microsoft.EntityFrameworkCore;

namespace Common.DAL.Extensions;

public static class DbContextExtensions
{
    private static readonly SemaphoreSlim Semaphore = new(1, 1);
    
    public static async Task ApplyMigrationAsync(this AppDbContext dbContext, CancellationToken ct)
    {
        await Semaphore.WaitAsync(TimeSpan.FromMinutes(1), ct);
        try
        {
            var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync(ct);
            if (pendingMigrations.Any())
                await dbContext.Database.MigrateAsync(ct);
        }
        finally
        {
            Semaphore.Release();
        }
    } 
}