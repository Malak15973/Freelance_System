﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Freelance_System.DAL.Entites
{
    [Table("SavedPosts")]
    public class SavedPosts
    {
        [Key]
        public int Id { get; set; }
        public string FreelancerId { get; set; }
        [ForeignKey("FreelancerId")]

        public ApplicationUser Freelancer { get; set; }

        public int PostId { get; set; }
        [ForeignKey("PostId")]

        public Post Post { get; set; }

    }
}
