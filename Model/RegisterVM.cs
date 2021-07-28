﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Freelance_System.Model
{
    public class RegisterVM
    { 
        [Required]
        [Remote(controller: "Account" ,action: "IsUserNameInUse")]
        public string UserName { get; set; } 
        [Required] 
        [EmailAddress]
        [Remote(controller: "Account", action: "IsEmailInUse")]
        public string Email { get; set; }
        [Required] 
        [MinLength(3,ErrorMessage ="Min Len 3 ")]
        public string Password { get; set; }
        public string RoleId { get; set; }
    }
}
