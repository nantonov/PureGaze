using Microsoft.Extensions.DependencyInjection;
using Notification.Application.Interfaces;
using Notification.Infrastructure.Repositories;
using Notification.Infrastructure.Senders;

namespace Notification.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IEmailRepository, EmailRepository>();
        services.AddScoped<IEmailSender, MockSender>();
        
        return services;
    }
}
