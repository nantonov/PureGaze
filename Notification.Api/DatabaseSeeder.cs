using Common.DAL;
using Common.Data.Enums;
using Common.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Notification.Api;

public static class DatabaseSeeder
{
    public static async Task SeedDataAsync(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
            return;

        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (await context.Emails.AnyAsync())
            return;

        var emails = new List<Email>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Subject = "High Priority Test",
                Body = "Body of high priority email",
                To = "test-high@example.com",
                From = "system@example.com",
                Priority = EmailPriority.High,
                Status = EmailStatus.InQueue,
                CreatedAt = DateTime.UtcNow.AddMinutes(-10)
            },
            new()
            {
                Id = Guid.NewGuid(),
                Subject = "Normal Priority Test",
                Body = "Body of normal priority email",
                To = "test-normal@example.com",
                From = "system@example.com",
                Priority = EmailPriority.Normal,
                Status = EmailStatus.InQueue,
                CreatedAt = DateTime.UtcNow.AddMinutes(-5)
            },
            new()
            {
                Id = Guid.NewGuid(),
                Subject = "Failed Email Test",
                Body = "Body of a previously failed email",
                To = "test-failed@example.com",
                From = "system@example.com",
                Priority = EmailPriority.Low,
                Status = EmailStatus.Failed,
                RetryCount = 1,
                CreatedAt = DateTime.UtcNow.AddHours(-1)
            },
            new()
            {
                Id = Guid.NewGuid(),
                Subject = "Low Priority Test",
                Body = "Body of low priority email",
                To = "test-low@example.com",
                From = "system@example.com",
                Priority = EmailPriority.Low,
                Status = EmailStatus.InQueue,
                CreatedAt = DateTime.UtcNow
            }
        };

        await context.Emails.AddRangeAsync(emails);
        await context.SaveChangesAsync();
    }
}
