using PureGaze.API.ExceptionHandlers;

namespace PureGaze.API.Configurations;

public static class ExceptionsConfig
{
    public static WebApplicationBuilder ExceptionsBuild(this WebApplicationBuilder builder)
    {
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        
        builder.Services.AddProblemDetails();

        return builder;
    }
}