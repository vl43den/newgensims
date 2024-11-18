using Microsoft.EntityFrameworkCore;
using UserApi.Models;


namespace UserApi.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public required DbSet<User> Users { get; set; }
    }
}
