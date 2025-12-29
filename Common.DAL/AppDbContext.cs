using Common.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Common.DAL;

public class AppDbContext(DbContextOptions<AppDbContext> options) 
    : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
    
    public DbSet<Employee> Employees { get; set; }
    
    public DbSet<ManagerialLevel> ManagerialLevels { get; set; }
    
    public DbSet<ProcessConfirmationStatus> ProcessConfirmationStatuses { get; set; }
    
    public DbSet<ProfessionalLevel> ProfessionalLevels { get; set; }

    public DbSet<Email> Emails { get; set; }

    public DbSet<AssessmentRequest> AssessmentRequests { get; set; }
}