using Microsoft.EntityFrameworkCore;

namespace Management.Infrastructure.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : DbContext(options)
{
    
}