using Microsoft.EntityFrameworkCore;
using PureGaze.Infrastructure.Database;

namespace PureGaze.API;

public static class DatabaseChecker
{
    public static async Task CheckDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
        if (pendingMigrations.Any())
            await dbContext.Database.MigrateAsync();

    }
}