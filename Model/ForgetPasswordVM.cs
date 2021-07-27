using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Freelance_System.Model
{
    public class ForgetPasswordVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
