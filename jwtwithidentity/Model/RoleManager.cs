using System.ComponentModel.DataAnnotations;

namespace jwtwithidentity.Model
{
    public class RoleManager
    {
        public int Id { get; set; }


        [Required]
        [Display(Name = "User")]
        public int UserId { get; set; }

        public Role Role { get; set; }

        [Required]
        [Display(Name = "Role")]
        public int RoleId { get; set; }
    }
}
