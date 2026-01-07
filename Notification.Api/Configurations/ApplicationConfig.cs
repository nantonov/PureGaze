using Notification.Application.Abstractions.Services;
using Notification.Application.Services;

namespace Notification.API.Configurations;

public static class ApplicationConfig
{
    public static WebApplicationBuilder СonfigureApplication(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IEmailService, EmailService>();

        return builder;
    }
}