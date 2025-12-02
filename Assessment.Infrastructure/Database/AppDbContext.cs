using Microsoft.EntityFrameworkCore;

namespace Assessment.Infrastructure.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) 
    : DbContext(options)
{
}