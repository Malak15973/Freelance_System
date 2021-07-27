using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freelance_System.Model
{
    public class ProfileVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Number { get; set; }
        public IFormFile Photo { get; set; }
        public string PhotoName { get; set; }
    }
}
