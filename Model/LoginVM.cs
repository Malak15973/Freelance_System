﻿using System.ComponentModel.DataAnnotations;

namespace Freelance_System.Model
{
    public class LoginVM
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public bool RemeberMe { get; set; }
    }
}
