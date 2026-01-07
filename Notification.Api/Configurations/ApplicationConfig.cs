using Notification.Application.Abstractions.Services;
using Notification.Application.Services;
using Notification.Infrastructure.Exceptions;

namespace Notification.API.Configurations;

public static class ApplicationConfig
{
    public static WebApplicationBuilder СonfigureApplication(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IEmailService, EmailService>();

        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();

        return builder;
    }
}
