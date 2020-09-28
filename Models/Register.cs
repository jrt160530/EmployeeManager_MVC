using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager_MVC.Models
{
    public class Register
    {
        [Required]
        [Display(Name ="User Name")]
        public string UserName { get; set; }
        
        [Required]
        [Display(Name="Password")]
        public string Password { get; set; }
        
        [Required]
        [Compare("Password")]
        [Display(Name ="Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name ="Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "FullName")]
        public string FullName{ get; set; }

        [Required]
        [Display(Name = "BirthDate")]
        public DateTime BirthDate { get; set; }
    }
}
