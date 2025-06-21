using Entities;
using Microsoft.EntityFrameworkCore;

namespace Frontend.Authentication
{
    public class AppDbContext:DbContext
{
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<User> GoogleUsers { get; set; }

    }
}
