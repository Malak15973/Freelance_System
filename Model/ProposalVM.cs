using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Freelance_System.Model
{
    public class ProposalVM
    {
        public int Id { get; set; }
        public string FreelancerId { get; set; }
        public int PostId { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsAccepted { get; set; }
        [Required]
        [StringLength(50)] 
        public string Description { get; set; }

    }
}
