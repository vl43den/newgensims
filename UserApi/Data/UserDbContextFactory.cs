using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using UserApi.Models;

namespace UserApi.Data
{
    public class UserDbContextFactory : IDesignTimeDbContextFactory<UserDbContext>
    {
        public UserDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<UserDbContext>();

            // Use SQLite or other connection string as required
            optionsBuilder.UseSqlite("Data Source=users.db");

            return new UserDbContext(optionsBuilder.Options);
        }
    }
}
