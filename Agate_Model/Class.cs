using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Agate_Model
{
    public class Class
    {
        [Display(Name = "Class Number")]
        public int ClassNumber { get; set; }
        public int Grade { get; set; }

        public List<Student> Students { get; set; }
    }
}
