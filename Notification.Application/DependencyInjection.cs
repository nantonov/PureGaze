using Microsoft.Extensions.DependencyInjection;
using Notification.Application.Services;
using Notification.Application.Strategies;

namespace Notification.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<NotificationService>();
        services.AddTransient<HighPriorityNotificationStrategy>();
        services.AddTransient<StandardNotificationStrategy>();
        
        return services;
    }
}
