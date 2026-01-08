using Assessment.Application.Abstractions.Infrastructure;
using Assessment.Application.Abstractions.Services;
using Assessment.Infrastructure.Repositories;
using Common.DAL;
using Microsoft.EntityFrameworkCore;

namespace Assessment.API.Configurations;

public static class DatabaseConfig
{
    public static WebApplicationBuilder DatabasesBuilder(this WebApplicationBuilder builder)
    {
        var dbSection = 
            builder.Configuration.GetSection(AppDbOptions.SectionName)
                .Get<AppDbOptions>();
        
        builder.Services.AddDbContextFactory<AppDbContext>(options =>
        {
            options.UseSqlServer(dbSection?.ConnectionString,
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: dbSection?.MaxRetryCount ?? 3,
                        maxRetryDelay: TimeSpan.FromSeconds(dbSection?.MaxRetryDelaySecond ?? 5),
                        errorNumbersToAdd: null);
                });
        });

        builder.Services.AddScoped<IAssessmentRequestRepository, AssessmentRequestRepository>();
        
        return builder;
    }
}