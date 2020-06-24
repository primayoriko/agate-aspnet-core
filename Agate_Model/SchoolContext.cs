using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Pomelo.EntityFrameworkCore.MySql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Agate_Model
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options)
            : base(options)
        {

        }

        public DbSet<Student> Student { get; set; }

        public DbSet<Class> Class { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Student>().Property(s => s.StudentId)
            //.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            modelBuilder.Entity<Student>()
                .HasKey(s => s.StudentId);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.CurrentClass)
                .WithMany(c => c.Students)
                .HasForeignKey(s => new { s.Grade, s.ClassNumber });
            //.HasPrincipalKey( c => new { c.Grade, c.ClassNumber });

            modelBuilder.Entity<Class>()
                .HasKey(c => new { c.Grade, c.ClassNumber });
        }
    }
}
