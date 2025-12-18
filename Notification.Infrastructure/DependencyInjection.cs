using Microsoft.Extensions.DependencyInjection;
using Notification.Application.Contracts;
using Notification.Infrastructure.Repositories;
using Notification.Infrastructure.Senders;
using Notification.Infrastructure.Workers;

namespace Notification.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IEmailRepository, EmailRepository>();
        services.AddScoped<IEmailSender, MockSender>();
        
        //services.AddHostedService<HighPriorityWorker>();
        //services.AddHostedService<MediumPriorityWorker>();
        //services.AddHostedService<LowPriorityWorker>();
        
        return services;
    }
}
