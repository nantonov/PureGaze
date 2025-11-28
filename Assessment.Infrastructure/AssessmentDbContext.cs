using Microsoft.EntityFrameworkCore;

namespace Assessment.Infrastructure;

public class AssessmentDbContext : DbContext
{
    public AssessmentDbContext(DbContextOptions<AssessmentDbContext> options)
        : base(options)
    {
    }
}