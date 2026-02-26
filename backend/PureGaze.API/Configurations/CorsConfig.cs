using PureGaze.Infrastructure.Cors;

namespace PureGaze.API.Configurations;

public static class CorsConfig
{
    public static WebApplicationBuilder CorsBuild(this WebApplicationBuilder builder)
    {
        builder.Services.AddOptions<CorsOptions>()
            .Bind(builder.Configuration.GetSection(CorsOptions.SectionName));

        builder.Services.AddCors(options =>
        {
            var corsOptions =
                builder.Configuration.GetSection(CorsOptions.SectionName)
                    .Get<CorsOptions>()!;

            options.AddPolicy(CorsOptions.PolicyName,
                policy => policy.WithOrigins(corsOptions.Origins)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
        });

        return builder;
    }
}