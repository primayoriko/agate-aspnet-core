using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MVCApp.Models
{
    // This template class is optional, and only used when you use Identity service to make authentication in ASP NET Core
    // This class is used for make model in Register Form in the View 
    public class TemplateRegisterViewModelClass
    {
        // This template is based on TemplateIdentityUser class, so make sure to set the field below based on your own IdentityUser class
        [PersonalData]
        [Key]
        [Display(Name = "ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { set; get; }

        [Required, MaxLength(256)]
        public string Username { set; get; }

        [Required, MaxLength(256)]
        public string Email { set; get; }

        [PersonalData]
        [Required, MaxLength(256)]
        public string Name { set; get; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
