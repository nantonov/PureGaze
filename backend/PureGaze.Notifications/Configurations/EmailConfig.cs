using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Infrastructure.Notifications.Emails;
using PureGaze.Infrastructure.Notifications.Queue;

namespace PureGaze.Notifications.Configurations;

public static class EmailConfig
{
    public static IHostApplicationBuilder AddEmail(this IHostApplicationBuilder builder)
    {
        builder.Services.Configure<SmtpOptions>(
            builder.Configuration.GetSection(SmtpOptions.SectionName));

        builder.Services.AddSingleton<IEmailSender, EmailSender>();

        builder.Services.Configure<NotificationsQueueOptions>(
            builder.Configuration.GetSection(NotificationsQueueOptions.SectionName));

        return builder;
    }
}
