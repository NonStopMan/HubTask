using HUB.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace NetCore.Data
{
    public class PostContext : DbContext
    {
        public PostContext(DbContextOptions<PostContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var connection = @"Data Source=hubtask.database.windows.net;Database=HubTask;Trusted_Connection=False;User ID = hubtaskadmin;Password = P@ssw0rd ;Encrypt = True; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
            //optionsBuilder.UseSqlServer(connection);
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostReadByUser>().HasKey(c => new { c.PostId, c.UserId });
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<PostReadByUser> PostsReadyByUsers { get; set; }

    }
}