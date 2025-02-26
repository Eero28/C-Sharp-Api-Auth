using Microsoft.EntityFrameworkCore;
using UserAuth.Entities;

namespace UserAuth.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Review> Reviews { get; set; }

    }
}
