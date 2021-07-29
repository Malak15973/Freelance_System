using System.ComponentModel.DataAnnotations; 

namespace Freelance_System.Model
{
    public class ResetPasswordVM
    {
        public string Token { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Min Len 3 ")]
        public string Password { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Min Len 3 ")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

    }
}
