using Notification.Application.Services;
using Notification.Application.Services.Interfaces;

namespace Notification.API.Configurations;

public static class ApplicationConfig
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<INotificationService, NotificationService>();
        return services;
    }
}
