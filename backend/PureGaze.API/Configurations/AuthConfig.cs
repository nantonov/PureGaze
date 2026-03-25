using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PureGaze.API.Providers;
using PureGaze.Application.Abstractions.Providers;
using PureGaze.Infrastructure.Auth;

namespace PureGaze.API.Configurations;

public static class AuthConfig
{
    public static WebApplicationBuilder AuthConfigBuild(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ICurrentUserContextProvider, CurrentUserContextProvider>();
        builder.Services.AddScoped<IClaimsTransformation, DbRoleClaimsTransformation>();

        builder.Services.AddOptions<JwtOptions>()
            .Bind(builder.Configuration.GetSection(JwtOptions.SectionName));

        builder.Services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var jwtOptions = builder.Configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>()!;

                options.Authority = jwtOptions.Issuer;
                options.Audience = jwtOptions.Audience;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(5),
                };
            });

        return builder;
    }
}