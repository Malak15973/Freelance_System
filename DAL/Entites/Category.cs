using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Freelance_System.DAL.Entites
{
    [Table("Category")]
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
