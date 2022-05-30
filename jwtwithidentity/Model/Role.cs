using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace jwtwithidentity.Model
{
    public class Role
    {
        public int RoleId { get; set; }

        [Display(Name = "Role")]
        public string RoleName { get; set; }


    }
}
