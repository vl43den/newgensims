using Microsoft.EntityFrameworkCore;
using UserApi.Models;

namespace UserApi.Data
{
    public class IncidentDbContext : DbContext
    {
        public DbSet<Incident> Incidents { get; set; }

        public IncidentDbContext(DbContextOptions<IncidentDbContext> options)
            : base(options)
        {
        }
    }
}
