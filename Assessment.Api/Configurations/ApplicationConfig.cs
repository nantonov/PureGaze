using Assessment.Application.Abstractions.Services;
using Assessment.Application.Services;

namespace Assessment.Api.Configurations;

public static class ApplicationConfig
{
    public static WebApplicationBuilder СonfigureApplication(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IAssessmentRequestService, AssessmentRequestService>();
        
        return builder;
    }
}