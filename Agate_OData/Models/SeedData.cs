using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Agate_OData
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new SchoolContext(
                serviceProvider.GetRequiredService<DbContextOptions<SchoolContext>>()))
            {
                if (context.Class.Any() && context.Student.Any())
                {
                    return;
                }
                else
                {
                    context.Class.AddRange(
                        new Class
                        {
                            Grade = 1
                        },
                        new Class
                        {
                            Grade = 8
                        },
                        new Class
                        {
                            Grade = 11
                        },
                        new Class
                        {
                            Grade = 7
                        }
                    );

                    context.Student.AddRange(
                        new Student
                        {
                            StudentId = 12,
                            Name = "Jon",
                            Grade = 7
                        },
                        new Student
                        {
                            StudentId = 1,
                            Name = "Nathan",
                            Grade = 8
                        },
                        new Student
                        {
                            StudentId = 10,
                            Name = "Albert",
                            Grade = 11
                        }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}
