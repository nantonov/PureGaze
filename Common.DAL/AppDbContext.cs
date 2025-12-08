using Common.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Common.DAL;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Employee> Employees { get; set; }
    
    public DbSet<ManagerialLevel> ManagerialLevels { get; set; }
    
    public DbSet<MeetingRequestStatus> MeetingRequestStatuses { get; set; }
    
    public DbSet<ProcessConfirmationStatus> ProcessConfirmationStatuses { get; set; }
    
    public DbSet<ProfessionalLevel> ProfessionalLevels { get; set; }
    
    public DbSet<SkillLevel> SkillLevels { get; set; }
    
    public DbSet<YesNoOtherOption> YesNoOtherOptions { get; set; }
}