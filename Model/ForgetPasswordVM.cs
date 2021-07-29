using System.ComponentModel.DataAnnotations;

namespace Freelance_System.Model
{
    public class ForgetPasswordVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
