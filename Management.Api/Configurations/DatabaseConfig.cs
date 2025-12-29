using Common.DAL;
using Microsoft.EntityFrameworkCore;

namespace Management.Api.Configurations;

public static class DatabaseConfig
{
    public static WebApplicationBuilder ConfigureDatabases(this WebApplicationBuilder builder)
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
        
        return builder;
    }
}