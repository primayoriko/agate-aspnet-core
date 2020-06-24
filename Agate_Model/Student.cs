using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Agate_Model
{
    public class Student
    {
        [Display(Name = "Student ID")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StudentId { get; set; }
        public String Name { get; set; }

        /*[Display(Name = "Father Name")]
        public String FatherName { get; set; }*/
        
        [Display(Name = "Class Number")]
        public int ClassNumber { get; set; }
        public int Grade { get; set; }

        public Class CurrentClass { get; set; }
    }
}
