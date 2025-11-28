using Assessment.Common.Options;

namespace Assessment.API.Configs;

public static class PostgreHealthCheckConfig
{
    public static WebApplicationBuilder ConfigPostgreHealthCheck(this WebApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks()
            .AddNpgSql(builder.Configuration
                .GetSection(PostgreOptions.SectionName)
                .Get<PostgreOptions>()!.ConnectionString);

        return builder;
    }
}
