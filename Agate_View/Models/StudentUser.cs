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
    }
}
