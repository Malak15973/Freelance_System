using Freelance_System.DAL.Entites;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Freelance_System.DAL.Database
{
    public class DbContainer : IdentityDbContext<ApplicationUser>
    {
        public DbContainer(DbContextOptions<DbContainer> opts) : base(opts) { }

        public DbSet<Post> Post { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<SavedPosts> SavedPosts { get; set; }
        public DbSet<Proposal> Proposal { get; set; }
        public DbSet<Rate> Rate { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Rate>()
                .HasOne(r => r.Freelancer)
                .WithMany(r => r.Rate)
                .HasForeignKey(r => r.FreelancerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Rate>()
                .HasOne(r => r.Post)
                .WithMany(r => r.Rate)
                .HasForeignKey(r => r.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<SavedPosts>()
                .HasOne(r => r.Freelancer)
                .WithMany(r => r.SavedPosts)
                .HasForeignKey(r => r.FreelancerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<SavedPosts>()
                .HasOne(r => r.Post)
                .WithMany(r => r.SavedPosts)
                .HasForeignKey(r => r.PostId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Proposal>()
                .HasOne(r => r.Freelancer)
                .WithMany(r => r.Proposal)
                .HasForeignKey(r => r.FreelancerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Proposal>()
                .HasOne(r => r.Post)
                .WithMany(r => r.Proposal)
                .HasForeignKey(r => r.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Rate>().HasKey(r => new { r.FreelancerId, r.PostId });
        }
    }
}
