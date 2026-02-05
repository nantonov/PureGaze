using Microsoft.EntityFrameworkCore;
using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Infrastructure.Database;
using PureGaze.Infrastructure.Database.Repositories;

namespace PureGaze.API.Configurations;

public static class DatabaseConfig
{
    public static WebApplicationBuilder DatabasesBuild(this WebApplicationBuilder builder)
    {
        builder.Services.AddOptions<AppDbOptions>()
            .Bind(builder.Configuration.GetSection(AppDbOptions.SectionName));

        builder.Services.AddDbContextFactory<AppDbContext>(options =>
        {
            var dbOptions =
                builder.Configuration.GetSection(AppDbOptions.SectionName)
                    .Get<AppDbOptions>();

            options.UseSqlServer(dbOptions?.ConnectionString,
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: dbOptions?.MaxRetryCount ?? 3,
                        maxRetryDelay: TimeSpan.FromSeconds(dbOptions?.MaxRetryDelaySecond ?? 5),
                        errorNumbersToAdd: null);
                });
        });

        builder.Services.AddScoped<IEmailRepository, EmailRepository>();
        builder.Services.AddScoped<ICodeRepository, CodeRepository>();
        builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        builder.Services.AddScoped<IAssessmentRequestRepository, AssessmentRequestRepository>();
        builder.Services.AddScoped<IAssessmentRepository, AssessmentRepository>();
        builder.Services.AddScoped<ITemplateRepository, TemplateRepository>();
        builder.Services.AddScoped<ISubtopicRepository, SubtopicRepository>();
        builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
        builder.Services.AddScoped<IAnswerRepository, AnswerRepository>();
        builder.Services.AddScoped<IAssessmentStageRepository, AssessmentStageRepository>();

        builder.Services.Scan(scan => scan
            .FromApplicationDependencies()
            .AddClasses(c => c.AssignableTo(typeof(IDictionaryRepository<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime()
        );

        return builder;
    }
}