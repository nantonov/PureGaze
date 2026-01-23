using PureGaze.API;
using PureGaze.API.Configurations;
using PureGaze.Infrastructure.Cors;

var builder = WebApplication.CreateBuilder(args);

IHostEnvironment env = builder.Environment;

builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.AuthConfigBuild();
builder.CorsBuild();
builder.RequestsBuild();
builder.DatabasesBuild();
builder.BackgroundWorkersBuild();
builder.ExceptionsBuild();
builder.ProvidersBuild();

var app = builder.Build();

await app.CheckDatabase();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(x =>
    {
        x.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi3_1;
    });
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(CorsOptions.PolicyName);
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();