using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Data
{
    // Ändere die Basisklasse zu IdentityDbContext<User>
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Zusätzliche DbSets können hier hinzugefügt werden, falls nötig
        public DbSet<User> Users { get; set; }
    }
}
