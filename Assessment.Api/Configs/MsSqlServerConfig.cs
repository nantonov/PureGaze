using Assessment.Common.Options;
using Assessment.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Assessment.API.Configs;

public static class MsSqlServerConfig
{
    public static WebApplicationBuilder ConfigMsSqlServerConnection(this WebApplicationBuilder builder)
    {
        var msSqlServerOptions = builder.Configuration.GetSection(MsSqlServerOptions.SectionName)
            .Get<MsSqlServerOptions>() ?? throw new InvalidOperationException("MsSqlServerOptions configuration section is missing.");

        builder.Services.AddDbContext<AssessmentDbContext>(options =>
        {
            options.UseSqlServer(msSqlServerOptions?.ConnectionString);
        });

        builder.Services.AddHealthChecks()
            .AddSqlServer(msSqlServerOptions.ConnectionString);
            

        return builder;
    }
}
