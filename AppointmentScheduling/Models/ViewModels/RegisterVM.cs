using System;
using System.ComponentModel.DataAnnotations;

namespace AppointmentScheduling.Models.ViewModels
{
    public class RegisterVM
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Role Name")]
        public string RoleNme { get; set; }
    }
}
