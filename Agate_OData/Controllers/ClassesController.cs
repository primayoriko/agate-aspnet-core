using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agate_Model;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Agate_OData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        [EnableQuery]
        public IActionResult Get()
        {
            var classes = _context.Class;
            return Ok(classes);
        }

        // odata/Classes(grade=1, classNumber=1)
        //[EnableQuery(PageSize = 20, AllowedQueryOptions = AllowedQueryOptions.All)]
        //[ODataRoute("{grade}/{classNumber}")]
        [EnableQuery]
        public IActionResult Get([FromODataUri]string grade, [FromODataUri]string classNumber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            var classReq = _context.Class.Where(c => c.Grade == Int32.Parse(grade) && c.ClassNumber == Int32.Parse(classNumber)).FirstOrDefault();
            if(classReq == null)
            {
                return NotFound();
            }

            return Ok(classReq);
        }

        // GET: api/<ClassesController>
        /*[HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }*/
    }
}
