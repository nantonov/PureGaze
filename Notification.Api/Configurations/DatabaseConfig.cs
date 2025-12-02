using Notification.Infrastructure;
using Notification.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Notification.API.Configurations;
public static class DatabaseConfig
{
    public static WebApplicationBuilder DatabasesBuilder(this WebApplicationBuilder builder)
    {
        var appDbOptions = builder.Configuration.GetSection(AppDbOptions.SectionName)
            .Get<AppDbOptions>() ?? throw new InvalidOperationException("DatabaseOptions configuration section is missing.");

        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(appDbOptions?.ConnectionString);
        });

        builder.Services.AddHealthChecks()
            .AddSqlServer(appDbOptions.ConnectionString);
        
        return builder;
    }
}
