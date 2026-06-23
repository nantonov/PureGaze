using Microsoft.EntityFrameworkCore;
using PureGaze.Infrastructure.Database;

namespace PureGaze.API;

public static class DatabaseChecker
{
    public static async Task CheckDatabase(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();
        AppDbContext dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        IEnumerable<string> pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
        if (pendingMigrations.Any())
            await dbContext.Database.MigrateAsync();

    }
}