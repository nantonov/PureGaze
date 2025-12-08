using Common.DAL;

namespace Assessment.Api;

public static class DatabaseChecker
{
    public static async Task CheckDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        await dbContext.ApplyMigrationAsync(CancellationToken.None);
    }
}