using PureGaze.API;
using PureGaze.API.Configurations;
using PureGaze.Infrastructure.Cors;

var builder = WebApplication.CreateBuilder(args);

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

var app = builder.Build();

await app.CheckDatabase();

app.UseSwaggerForDevelopment();
app.UseHttpsRedirection();
app.UseCors(CorsOptions.PolicyName);
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/ping", () => "pong").AllowAnonymous();

app.MapControllers();

app.Run();