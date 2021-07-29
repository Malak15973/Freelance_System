using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Freelance_System.Model
{
    public class ProfileVM
    {
        public string Id { get; set; }
        [Required]
        [Remote(controller: "Home", action: "IsUserNameInUse")]
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Number { get; set; }
        public IFormFile Photo { get; set; }
        public string PhotoName { get; set; }
    }
}
