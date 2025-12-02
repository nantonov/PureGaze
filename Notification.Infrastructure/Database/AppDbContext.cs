using Microsoft.EntityFrameworkCore;

namespace Notification.Infrastructure.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : DbContext(options)
{
    
}