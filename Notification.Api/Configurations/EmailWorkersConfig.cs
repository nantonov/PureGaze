using Microsoft.Extensions.Options;
using Notification.Application.Abstractions.Infrastructure;
using Notification.Infrastructure.Providers;
using Notification.Infrastructure.Workers;

namespace Notification.API.Configurations;

public static class EmailWorkersConfig
{
    public static WebApplicationBuilder СonfigureEmailWorkers(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<RetryPolicyOptions>(
            builder.Configuration.GetSection(RetryPolicyOptions.SectionName));
        
        builder.Services.AddHostedService(conf =>
        {
            var scopeFactory = 
                conf.GetRequiredService<IServiceScopeFactory>();
            
            var logger = 
                conf.GetRequiredService<ILoggerFactory>()
                    .CreateLogger<EmailWorker>();

            var options = conf.GetRequiredService<IOptions<RetryPolicyOptions>>();
            
            return new EmailWorker(scopeFactory, options, logger);
        });

        builder.Services.AddScoped<IEmailSender, EmailSender>();
        
        return builder;
    }
}