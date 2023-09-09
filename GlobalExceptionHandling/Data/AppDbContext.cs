using GlobalExceptionHandling.Models;
using Microsoft.EntityFrameworkCore;

namespace GlobalExceptionHandling.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base(options)
        {
            
        }

        public DbSet<Driver> Drivers { get; set; }
    }
}
