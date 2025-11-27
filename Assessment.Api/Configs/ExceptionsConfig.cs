using Assessment.Infrastructure.Handlers;

namespace Assessment.API.Configs;

public static class ExceptionsConfig
{
    public static WebApplicationBuilder ConfigExceptionsHandling(this WebApplicationBuilder builder)
    {
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();
        
        return builder;
    }
}
