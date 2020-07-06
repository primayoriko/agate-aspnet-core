using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Agate_Model
{
    public class Class
    {
        [DataMember]
        [Key]
        [Display(Name = "Class Number")]
        public int ClassNumber { get; set; }

        [DataMember]
        [Key]
        public int Grade { get; set; }

        public List<Student> Students { get; set; }
    }
}
