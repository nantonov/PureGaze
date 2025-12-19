using Notification.Application.Contracts;
using Notification.Infrastructure.Repositories;
using Notification.Infrastructure.Workers;

namespace Notification.API.Configurations;

public static class InfrastructureConfig
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RetryPolicyOptions>(configuration.GetSection("RetryPolicy"));
        services.AddScoped<IEmailRepository, EmailRepository>();
        
        return services;
    }
}
