using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Freelance_System.DAL.Entites
{
    public class ApplicationUser : IdentityUser
    {
        public string PhotoPath { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<SavedPosts> SavedPosts { get; set; }
        public virtual ICollection<Proposal> Proposal { get; set; }
        public virtual ICollection<Rate> Rate { get; set; }
    }
}
