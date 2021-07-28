using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Freelance_System.DAL.Entites
{
    [Table("Post")]
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public float Budget { get; set; }

        public bool IsAccepted { get; set; }
        public string ClientId { get; set; }
        [ForeignKey("ClientId")]
        public ApplicationUser Client { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Categories { get; set; }

        public virtual ICollection<SavedPosts> SavedPosts { get; set; }
        public virtual ICollection<Proposal> Proposal { get; set; }
        public virtual ICollection<Rate> Rate { get; set; }


    }
}
