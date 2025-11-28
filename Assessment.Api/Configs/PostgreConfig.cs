using Assessment.Common.Options;
using Assessment.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Assessment.API.Configs;

public static class PostgreConfig
{
    public static WebApplicationBuilder ConfigPostgreConnection(this WebApplicationBuilder builder)
    {
        var postgreOptions = builder.Configuration.GetSection(PostgreOptions.SectionName)
            .Get<PostgreOptions>();

        builder.Services.AddDbContext<AssessmentDbContext>(options =>
        {
            options.UseNpgsql(postgreOptions?.ConnectionString);
        });
            

        return builder;
    }
}
