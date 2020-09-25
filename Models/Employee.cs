using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace EmployeeManager_MVC.Models
{
    [Table("Employees")]
    public class Employee
    {
        [Column("EmployeeID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Employee ID is required")]
        [Display(Name ="Employee ID")]
        public int EmployeeID { get; set; }

        [Column("FirstName")]
        [Display(Name= "First Name")]
        [Required(ErrorMessage = "First name is required")]
        [StringLength(20, ErrorMessage = "First name must be less than 20 characters")]
        public string FirstName { get; set; }

        [Column("LastName")]
        [Display(Name ="Last Name")]
        [Required(ErrorMessage = "Last name is required")]
        [StringLength(30, ErrorMessage = "Last name must be less than 30 characters")]
        public string LastName{ get; set; }

        [Column("Title")]
        [Display(Name ="Title")]
        [Required(ErrorMessage = " TItle is required")]
        [StringLength(30, ErrorMessage = "Title must be less than 30 characters")]
        public string Title { get; set; }

        [Column("BirthDate")]
        [Display(Name ="Birthdate")]
        [Required(ErrorMessage = " Birthdate is required")]
        public DateTime BirthDate { get; set; }

        [Column("HireDate")]
        [Display(Name ="Hire Date")]
        [Required(ErrorMessage ="Hire date is required")]
        public DateTime HireDate { get; set; }

        [Column("County")]
        [Display(Name ="Country")]
        [Required(ErrorMessage ="Country is required")]
        [StringLength(30, ErrorMessage ="Country must be less than 30 characters")]
        public string Country { get; set; }

        [Column("Notes")]
        [Display(Name ="Notes")]
        [StringLength(500, ErrorMessage = "Notes must be less than 500 character")]
        public string Notes { get; set; }


    }
}
