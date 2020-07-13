using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Agate_OData.Controllers
{
    [ApiController]
    [Route("odata/[controller]")]
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

        //[HttpGet]
        [Route("Students")]
        [ActionName("Get")]
        [ODataRoute("Students")]
        [EnableQuery]
        public IActionResult Get()
        {
            var students = _context.Student;
            return Ok(students);
        }

        //[EnableQuery(PageSize = 20, AllowedQueryOptions = AllowedQueryOptions.All)]
        //[ODataRoute("Students/{id}")]
        /*[Route("Students({id})")]
        [ODataRoute("Students({id})")]
        [EnableQuery]
        public IActionResult Get([FromODataUri]int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var student = _context.Student.Where(c => c.StudentId == id).FirstOrDefault();  //.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }*/

        [HttpPost]
        [Route("Students")]
        [ODataRoute("Students")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult<Student>> Post([FromBody] Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _context.Student.Add(student);

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
            //"GetStudent", new { id = student.StudentId }, student
            return CreatedAtAction("Get", new { }, student);
        }

        [HttpPut]
        [Route("Students({id})")]
        [ODataRoute("Students({id})")]
        public async Task<ActionResult<Student>> Put(int id, Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                _context.Update(student);
                _context.Entry(student).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

            }

            return CreatedAtAction("Get", new { }, student);
        }


        [HttpPatch]
        [Route("Students({id})")]
        [ODataRoute("Students({id})")]
        public async Task<ActionResult<Student>> Patch([FromODataUri]int id, [FromBody] JsonPatchDocument<Student> studentPatch)
        {
            if(studentPatch != null && ModelState.IsValid)
            {
                try
                {
                    //var obj = JsonConvert.DeserializeObject<Student>(student);
                    //var jsonPatch = new JsonPatchDocument<Student>().Replace(x => x.Name, "Jonathan");
                    var student = await _context.Student.FindAsync(id);
                    studentPatch.ApplyTo(student);                    
                    _context.Update(student);
                    _context.Entry(student).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                } 
                catch (Exception)
                {
                    return BadRequest(ModelState);
                }

                return CreatedAtAction("Get", new { }, null);
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("Students({id})")]
        [ODataRoute("Students({id})")]
        public async Task<ActionResult<Student>> Delete([FromODataUri] int id)
        {
            var s = new Student();
            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Student.Remove(student);
            await _context.SaveChangesAsync();

            return student;
        }
    }
}
