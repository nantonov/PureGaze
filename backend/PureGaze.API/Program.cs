using PureGaze.API;
using PureGaze.API.Configurations;
using PureGaze.Infrastructure.Cors;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

IHostEnvironment env = builder.Environment;

builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

builder.AuthConfigBuild();
builder.AddSwagger();
builder.CorsBuild();
builder.RequestsBuild();
builder.DatabasesBuild();
builder.BackgroundWorkersBuild();
builder.ExceptionsBuild();
builder.ProvidersBuild();

WebApplication app = builder.Build();

await app.CheckDatabase();

app.UseSwaggerForDevelopment();
app.UseHttpsRedirection();
app.UseCors(CorsOptions.PolicyName);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();