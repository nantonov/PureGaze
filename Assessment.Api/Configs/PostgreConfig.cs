using Assessment.Common.Options;
using Assessment.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Assessment.API.Configs;

public static class PostgreConfig
{
    public static WebApplicationBuilder ConfigPostgreConnection(this WebApplicationBuilder builder)
    {
        var postgreOptions = builder.Configuration.GetSection(PostgreOptions.SectionName)
            .Get<PostgreOptions>() ?? throw new InvalidOperationException("PostgreOptions configuration section is missing.");

        builder.Services.AddDbContext<AssessmentDbContext>(options =>
        {
            options.UseNpgsql(postgreOptions?.ConnectionString);
        });

        builder.Services.AddHealthChecks()
            .AddNpgSql(postgreOptions.ConnectionString);
            

        return builder;
    }
}
