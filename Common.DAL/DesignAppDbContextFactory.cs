using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Common.DAL;

public class DesignAppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env}.json", true, true)
            .Build();

        var dbSection = builder
            .GetSection(AppDbOptions.SectionName) 
            .Get<AppDbOptions>();
        
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer(dbSection?.ConnectionString,
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: dbSection?.MaxRetryCount ?? 3,
                        maxRetryDelay: TimeSpan.FromSeconds(dbSection?.MaxRetryDelaySecond ?? 5),
                        errorNumbersToAdd: null);
                })
            .Options;

        return new AppDbContext(options);
    }
}