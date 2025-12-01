using Management.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Management.Api;

public static class DatabaseChecker
{
    public static void CheckDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var pendingMigrations = dbContext.Database.GetPendingMigrations();
        if (pendingMigrations.Any())
            dbContext.Database.Migrate();
    }
}