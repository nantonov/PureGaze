using Management.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Management.Api.Configurations;

public static class DatabaseConfig
{
    public static WebApplicationBuilder DatabasesBuilder(this WebApplicationBuilder builder)
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

        builder.Services.AddHealthChecks()
            .AddSqlServer(dbSection?.ConnectionString ?? "");
        
        return builder;
    }
}