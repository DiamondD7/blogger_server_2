using bloggerServer.Model;
using Microsoft.EntityFrameworkCore;

namespace bloggerServer.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options) : base(options)
        {
        }


        public DbSet<User> Users { get; set; } 
        public DbSet<BlogPost> PostTable { get; set; }
        public DbSet<Hashtags> Hashtags { get; set; }
        public DbSet<SecurityQuestion> SecurityTable { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }

    }
}
