using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_Agate_01.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public String Name { get; set; }
        public int ClassNumber { get; set; }
        public int Grade { get; set; }
        public Student() { }
    }
}
