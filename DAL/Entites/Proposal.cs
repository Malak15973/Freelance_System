using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Freelance_System.DAL.Entites
{
    [Table("Proposal")]
    public class Proposal
    {
        [Key]
        public int Id { get; set; }
        public string FreelancerId { get; set; }
        [ForeignKey("FreelancerId")]

        public ApplicationUser Freelancer { get; set; }

        public int PostId { get; set; }
        [ForeignKey("PostId")]

        public Post Post { get; set; }

        public DateTime CreationDate { get; set; }
        public bool IsAccepted { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
    }

}
