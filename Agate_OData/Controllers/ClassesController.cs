using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Agate_OData.Controllers
{
    [ApiController]
    [Route("odata/[controller]")]
    public class ClassesController : ODataController
    {
        private readonly SchoolContext _context;
        public ClassesController(SchoolContext context)
        {
            _context = context;
            if (!context.Class.Any())
            {
                _context.Database.EnsureCreated();
            }
        }

        //[HttpGet("Classes", Name = "Get2")]
        [Route("Classes")]
        [ActionName("Get2")]
        [ODataRoute("Classes")]
        [EnableQuery]
        public IActionResult Get()
        {
            var classes = _context.Class;
            return Ok(classes);
        }

        // odata/Classes(grade=1, classNumber=1)
        //[EnableQuery(PageSize = 20, AllowedQueryOptions = AllowedQueryOptions.All)]
        // Note : jika pengen Classes(a={k}), maka harus ada atribut a

        /*[HttpGet("Classes({grade})", Name = "Get2")]
        [ActionName("GetClass")]
        //[Route("Classes({grade})")]
        [ODataRoute("Classes({grade})")]
        [EnableQuery]
        public IActionResult GetClass([FromODataUri] int grade)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //var classReq = _context.Class.Where(c => c.Grade == Int32.Parse(grade) && c.ClassNumber == Int32.Parse(classNumber)).FirstOrDefault();
            var classReq = _context.Class.Where(c => c.Grade == grade).FirstOrDefault();
            if (classReq == null)
            {
                return NotFound();
            }

            return Ok(classReq);
        }*/

        [HttpPost]
        [ODataRoute("Classes")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult<Student>> Post([FromBody] Class myclass)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _context.Class.Add(myclass);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                /*if (StudentExists(student.StudentId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }*/
            }

            //return RedirectToAction("Get2", "Classes");
            //return CreatedAtRoute("Get2", new { grade = myclass.Grade }, myclass);
            return CreatedAtAction("Get2", new { }, myclass);
        }

        [HttpPut]
        [Route("Classes({grade})")]
        [ODataRoute("Classes({grade})")]
        public async Task<ActionResult<Class>> Put(int grade, Class myclass)
        {
            return CreatedAtAction("Get2", new { }, myclass);
        }


    }
}
