using Microsoft.EntityFrameworkCore;

namespace UserApi.Models  // Ensure this matches the namespace you're using
{
    public class UserDbContext : DbContext
    {
        // Constructor that takes DbContextOptions
        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        { }

        // DbSet for the "Users" table
        public DbSet<User> Users { get; set; }
    }
}
