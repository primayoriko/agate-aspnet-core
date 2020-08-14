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
    // You can remove it if not needed
    // Used as a template for defining user data class
    public class TemplateIdentityUser : IdentityUser
    {
        // IdentityUser class already has fields that needed for basic usage, 
        // If you need to define additional field, just input as the example below
        [PersonalData]
        [Key]
        [Display(Name = "ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { set; get; }

        [PersonalData]
        [Required, MaxLength(256)]
        public string Name { set; get; }

    }
}
