using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Agate_View.Models
{
    public class StudentUser : IdentityUser
    {
        [PersonalData]
        [Key]
        [Display(Name = "Student ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StudentID { set; get; }

        [PersonalData]
        [Required, MaxLength(256)]
        public string Name { set; get; }

        [NotMapped]
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [NotMapped]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [NotMapped]
        [Display(Name = "Remember Me")]
        public bool Remember { set; get; }
    }
}
