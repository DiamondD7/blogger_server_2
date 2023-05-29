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
        public DbSet<BlogPost> Blogs { get; set; }
    }
}
