using Notification.Api;
using Notification.API.Configurations;
using Notification.Application;
using Notification.Application.Configurations;
using Notification.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
IHostEnvironment env = builder.Environment;

builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.Configure<RetryPolicyOptions>(builder.Configuration.GetSection("RetryPolicy"));

builder.Services.AddApplication();
builder.Services.AddInfrastructure();

builder.DatabasesBuilder();

var app = builder.Build();

await app.CheckDatabase();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(x =>
    {
        x.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi2_0;
    });
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
