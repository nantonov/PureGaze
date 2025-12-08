
using Assessment.Api;
using Assessment.API.Configurations;

var builder = WebApplication.CreateBuilder(args);

IHostEnvironment env = builder.Environment;

builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.ExceptionsBuilder();
builder.DatabasesBuilder();

var app = builder.Build();

await app.CheckDatabase();

app.UseExceptionHandler();

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
