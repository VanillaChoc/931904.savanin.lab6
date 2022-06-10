using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Web2._6.Models;

namespace Web2._6.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Post>? Posts { get; set; }

        public DbSet<ForumCategory>? Forums { get; set; }
        public DbSet<Topic>? Topics { get; set; }
        public DbSet<Reply>? Replies { get; set; }
        public DbSet<UserFile>? AttachedFiles { get; set; }
    }
}