using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ASP_Agate_01.Models
{
    public class ASP_Agate_01Context : DbContext
    {
        public ASP_Agate_01Context (DbContextOptions<ASP_Agate_01Context> options)
            : base(options)
        {
        }

        public DbSet<ASP_Agate_01.Models.Student> Student { get; set; }
    }
}
