using Microsoft.Extensions.DependencyInjection;

namespace Assessment.Common;

public static class CommonServiceExtension
{
    public static IServiceCollection AddCommonServices(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();

        return services;
    }
}
