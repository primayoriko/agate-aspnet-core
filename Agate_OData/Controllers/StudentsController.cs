using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.OData;
using Agate_Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Agate_OData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ODataController
    {
        private readonly SchoolContext _context;
        public StudentsController(SchoolContext context)
        {
            _context = context;
            if (!context.Class.Any())
            {
                _context.Database.EnsureCreated();
            }
        }

        [EnableQuery]
        public IActionResult Get()
        {
            var students = _context.Student;
            return Ok(students);
        }

        // localhost:port/odata/Classes(grade=1, classNumber=1)
        //[EnableQuery(PageSize = 20, AllowedQueryOptions = AllowedQueryOptions.All)]
        [EnableQuery]
        public IActionResult Get([FromODataUri] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var student = _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }
    }
}
