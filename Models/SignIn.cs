using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager_MVC.Models
{
    public class SignIn
    {
        [Required]
        [Display(Name="User Name")]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required]
        public string Password { get; set; }
        
        [Required]
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }


    }

}
