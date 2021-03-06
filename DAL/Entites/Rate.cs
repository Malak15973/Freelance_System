using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Freelance_System.DAL.Entites
{
    [Table("Rate")]
    public class Rate
    {
        public string FreelancerId { get; set; }
        [ForeignKey("FreelancerId")]
        public ApplicationUser Freelancer { get; set; }
        public int PostId { get; set; }
        [ForeignKey("PostId")]
        public Post Post { get; set; }
        [Range(0, 5)]
        public int FreelancerRate { get; set; }
    }
}
