using Management.Application.Abstractions.Providers;
using Management.Infrastructure.Integrations.Hrm;

namespace Management.Api.Configurations;

public static class HrmProviderConfig
{
    public static WebApplicationBuilder ConfigureHrmProvider(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<HrmOptions>(builder.Configuration.GetSection(key: HrmOptions.SectionName));

        var options = builder.Configuration.GetSection(HrmOptions.SectionName).Get<HrmOptions>();
        
        builder.Services.AddScoped<IHrmDataProvider, HrmDataProvider>();
        
        builder.Services.AddHttpClient(
            HrmOptions.EmployeeClientName,
            client =>
            {
                client.BaseAddress = new Uri(options?.EmployeeApiUrl ?? "");
            });

        builder.Services.AddHttpClient(
            HrmOptions.KeycloakClientName,
            client =>
            {
                client.BaseAddress = new Uri(options?.KeycloakUrl ?? "");
            });

        return builder;
    }
}
