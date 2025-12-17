using Microsoft.Extensions.DependencyInjection;
using Notification.Application.Services;
using Notification.Application.Strategies;

namespace Notification.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<NotificationService>();
        services.AddKeyedScoped<INotificationStrategy, HighPriorityNotificationStrategy>("high");
        services.AddKeyedScoped<INotificationStrategy, StandardNotificationStrategy>("standard");
        
        return services;
    }
}
