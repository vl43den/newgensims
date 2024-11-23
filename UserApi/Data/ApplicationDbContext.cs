using Microsoft.AspNetCore.Identity.EntityFrameworkCore; // Add this if not already present
using Microsoft.EntityFrameworkCore;
using UserApi.Models;

namespace UserApi.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<LogEntry> LogEntries { get; set; }  // Add this definition
    }
}
