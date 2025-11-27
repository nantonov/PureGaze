using Management.Application.Abstractions.Services;
using Management.Infrastructure.Integrations.Hrm;

namespace Management.Api.Configuration;

public static class HrmConfig
{
    public static WebApplicationBuilder ConfigHrmService(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<HrmOptions>(builder.Configuration.GetSection(key: HrmOptions.SectionName));

        var options = builder.Configuration.GetSection(HrmOptions.SectionName).Get<HrmOptions>();

        builder.Services.AddScoped<IHrmService, HrmService>();

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
