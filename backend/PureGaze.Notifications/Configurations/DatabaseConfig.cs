using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Infrastructure.Database;
using PureGaze.Infrastructure.Database.Repositories;

namespace PureGaze.Notifications.Configurations;

public static class DatabaseConfig
{
    public static IHostApplicationBuilder AddDatabase(this IHostApplicationBuilder builder)
    {
        builder.Services.Configure<AppDbOptions>(
            builder.Configuration.GetSection(AppDbOptions.SectionName));

        builder.Services.AddDbContextFactory<AppDbContext>(options =>
        {
            var connectionString = builder.Configuration[$"{AppDbOptions.SectionName}:ConnectionString"];
            options.UseSqlServer(connectionString);
        });

        builder.Services.AddScoped<IEmailRepository, EmailRepository>();

        return builder;
    }
}
