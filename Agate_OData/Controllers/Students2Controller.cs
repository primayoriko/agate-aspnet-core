using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Simple.OData.Client;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Agate_OData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Students2Controller : ControllerBase
    {
        //private readonly string _clientName = "https://localhost:44391/odata/";
        private readonly ODataClient _client;
        public Students2Controller()
        {
            _client = new ODataClient("https://localhost:44391/odata/");
        }
        // GET: api/<Students2Controller>
        [HttpGet]
        [ActionName("GetAll")]
        public async Task<ActionResult<IEnumerable<Student>>> Get()
        {
            //var client = new ODataClient(_clientName);
            var students = await _client
                                .For<Student>()
                                .FindEntriesAsync();
            return new ActionResult<IEnumerable<Student>>(students);
        }

        // GET api/<Students2Controller>/5
        [HttpGet("{id}")]
        [ActionName("Get")]
        public async Task<ActionResult<Student>> Get(int id)
        {
            //var client = new ODataClient(_clientName);
            var student = await _client
                                .For<Student>()
                                .Filter(x => x.StudentId == id)
                                .FindEntryAsync();
            return new ActionResult<Student>(student);
        }

        // POST api/<Students2Controller>
        [HttpPost]
        public async Task<ActionResult<Student>> Post([FromBody] Student student)
        {
            try
            {
                var client = _client
                                    .For<Student>()
                                    .Set(student);
                //var client = new ODataClient(_clientName);
                var addStudent = await client
                                    .InsertEntryAsync();
            }
            catch (Microsoft.OData.ODataException e)
            {

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return StatusCode(402);
            }

            return CreatedAtAction("GetAll", new { }, null);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<Student>> UpdateName(int id, [FromBody] string name)
        {
            try
            {
                await _client
                        .For<Student>()
                        .Key(id)
                        .Set(new { Name = name })
                        .UpdateEntryAsync();

            } catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return StatusCode(402);
                //return BadRequest();
            }
            return RedirectToAction("Get", new { id });
            //return CreatedAtAction("GetAll", new { }, await );
        }

        // PUT api/<Students2Controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Students2Controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
