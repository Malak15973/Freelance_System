using Freelance_System.DAL.Entites;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

         
    }
}
