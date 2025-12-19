using Common.Data.Enums;
using Microsoft.Extensions.Options;
using Notification.Infrastructure.Workers;

namespace Notification.API.Configurations;

public static class EmailWorkersConfig
{
    public static IServiceCollection AddEmailWorkers(this IServiceCollection services)
    {
        services.AddSingleton<IHostedService>(sp => 
        {
            var loggerFactory = sp.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger($"{nameof(EmailWorker)}.High");
            
            return new EmailWorker(
                sp.GetRequiredService<IServiceScopeFactory>(),
                sp.GetRequiredService<IOptions<RetryPolicyOptions>>(),
                logger,
                EmailPriority.High,
                TimeSpan.FromMinutes(5));
        });

        services.AddSingleton<IHostedService>(sp => 
        {
            var loggerFactory = sp.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger($"{nameof(EmailWorker)}.Normal");
            
            return new EmailWorker(
                sp.GetRequiredService<IServiceScopeFactory>(),
                sp.GetRequiredService<IOptions<RetryPolicyOptions>>(),
                logger,
                EmailPriority.Normal,
                TimeSpan.FromMinutes(10));
        });

        services.AddSingleton<IHostedService>(sp => 
        {
            var loggerFactory = sp.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger($"{nameof(EmailWorker)}.Low");
            
            return new EmailWorker(
                sp.GetRequiredService<IServiceScopeFactory>(),
                sp.GetRequiredService<IOptions<RetryPolicyOptions>>(),
                logger,
                EmailPriority.Low,
                TimeSpan.FromMinutes(20));
        });

        return services;
    }
}