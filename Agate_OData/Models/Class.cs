using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Agate_OData
{
    public class Class
    {
        public Class()
        {
            Students = new List<Student>();
        }

        /*[DataMember]
        [Key]
        public int ClassNumber { get; set; }*/

        [DataMember]
        [Key]
        public int Grade { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
