using System.ComponentModel.DataAnnotations;

namespace Freelance_System.Model
{
    public class CreateRoleVM
    {
        [Required]
        public string RoleName { get; set; }
    }
}
