using Microsoft.EntityFrameworkCore;

namespace newgensims.Models
{
    public class IncidentDbContext : DbContext
    {
        public IncidentDbContext(DbContextOptions<IncidentDbContext> options)
            : base(options)
        { }

        public DbSet<Incident> Incidents { get; set; }
    }
}
