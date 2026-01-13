using PureGaze.Application.Abstractions.Providers;
using PureGaze.Infrastructure.Integrations.Hrm;

namespace PureGaze.API.Configurations;

public static class HrmProviderConfig
{
    public static WebApplicationBuilder ProvidersBuild(this WebApplicationBuilder builder)
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