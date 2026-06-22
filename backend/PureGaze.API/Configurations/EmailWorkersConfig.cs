using Microsoft.Extensions.Options;
using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Infrastructure.Notifications.Emails;
using PureGaze.Infrastructure.Workers;

namespace PureGaze.API.Configurations;

public static class EmailWorkersConfig
{
    public static WebApplicationBuilder BackgroundWorkersBuild(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<SmtpOptions>(
            builder.Configuration.GetSection(SmtpOptions.SectionName));

        builder.Services.Configure<RetryPolicyOptions>(
            builder.Configuration.GetSection(RetryPolicyOptions.SectionName));

        builder.Services.AddHostedService(conf =>
        {
            IServiceScopeFactory scopeFactory =
                conf.GetRequiredService<IServiceScopeFactory>();

            ILogger<EmailWorker> logger =
                conf.GetRequiredService<ILoggerFactory>()
                    .CreateLogger<EmailWorker>();

            IOptions<RetryPolicyOptions> options = conf.GetRequiredService<IOptions<RetryPolicyOptions>>();

            return new EmailWorker(scopeFactory, options, logger);
        });

        builder.Services.AddSingleton<IEmailSender, EmailSender>();
        builder.Services.AddScoped<IEmailFactory, EmailFactory>();

        return builder;
    }
}