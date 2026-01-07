using Notification.Infrastructure.Exceptions;

namespace Notification.API.Configurations;

public static class ExceptionsConfig
{
    public static WebApplicationBuilder СonfigureExceptions(this WebApplicationBuilder builder)
    {
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();

        return builder;
    }
}
