using Assessment.Infrastructure.Handlers;

namespace Assessment.API.Configurations;

public static class ExceptionsConfig
{
    public static WebApplicationBuilder ExceptionsBuilder(this WebApplicationBuilder builder)
    {
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();
        
        return builder;
    }
}
