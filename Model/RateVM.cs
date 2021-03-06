using System;
using System.ComponentModel.DataAnnotations; 

namespace Freelance_System.Model
{
    public class RateVM
    {
        public string FreelancerId { get; set; }
        public int PostId { get; set; }
        [Required]
        [Range(1,5)]
        public int Rate { get; set; }
    }
}
