using Management.Application.Abstractions.Services;
using Management.Application.Services;

namespace Management.Api.Configurations;

public static class ServiceConfig
{
    public static WebApplicationBuilder ConfigureService(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IHrmService, HrmService>();
        
        return builder;
    }
}