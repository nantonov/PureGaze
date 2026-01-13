using PureGaze.Application.Requests;

namespace PureGaze.API.Configurations;

public static class RequestConfig
{
    public static WebApplicationBuilder RequestsBuild(this WebApplicationBuilder builder)
    {
        
        builder.Services.AddScoped<IRequestDispatcher, RequestDispatcher>();

        builder.Services.Scan(scan => scan 
            .FromApplicationDependencies() 
            .AddClasses(c => c.AssignableTo(typeof(IRequestHandler<>))) 
            .AsImplementedInterfaces() 
            .WithScopedLifetime() 
            .AddClasses(c => c.AssignableTo(typeof(IRequestHandler<,>))) 
            .AsImplementedInterfaces() 
            .WithScopedLifetime()
        );
        
        return builder;
    }
}