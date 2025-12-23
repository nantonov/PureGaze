using Common.DAL;
using Microsoft.EntityFrameworkCore;
using Notification.Application.Abstractions.Infrastructure;
using Notification.Infrastructure.Repositories;

namespace Notification.API.Configurations;

public static class DatabaseConfig
{
    public static WebApplicationBuilder СonfigureDatabases(this WebApplicationBuilder builder)
    {
        var dbSection = 
            builder.Configuration.GetSection(AppDbOptions.SectionName)
                .Get<AppDbOptions>();
        
        builder.Services.AddDbContextFactory<AppDbContext>(options =>
        {
            options.UseSqlServer(dbSection?.ConnectionString,
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: dbSection?.MaxRetryCount ?? 3,
                        maxRetryDelay: TimeSpan.FromSeconds(dbSection?.MaxRetryDelaySecond ?? 5),
                        errorNumbersToAdd: null);
                });
        });
        
        builder.Services.AddScoped<IEmailRepository, EmailRepository>();
        
        return builder;
    }
}
