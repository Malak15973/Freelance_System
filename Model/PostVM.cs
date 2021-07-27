using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Freelance_System.Model
{
    public class PostVM
    {
        
        public int Id { get; set; }
        [Required]
        [StringLength(50)] 
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public DateTime CreationDate { get; set; }
        [Required]
        [Range(1000,30000)]
        public float Budget { get; set; }
        public string ClientId { get; set; }
        public string ClientName { get; set; }

    }
}
