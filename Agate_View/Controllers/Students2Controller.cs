using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Agate_Model;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace Agate_View.Controllers
{
    public class Students2Controller : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public Students2Controller(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                "https://localhost:44392/api/Students");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string jsonData = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<IEnumerable<Student>>(jsonData);
                return View(data);
            } 
            else
            {
                return NotFound();
            }
        }
        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var request = new HttpRequestMessage(HttpMethod.Get,
               $"https://localhost:44392/api/Students/{id}");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string jsonData = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Student>(jsonData);
                if(data == null)
                {
                    return NotFound();
                }
                return View(data);
            }
            else
            {
                return NotFound();
            }
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,Name,ClassNumber,Grade")] Student student)
        {
            if (ModelState.IsValid)
            {
                var studentJson = new StringContent(
                    System.Text.Json.JsonSerializer.Serialize<Student>(student),
                    Encoding.UTF8, "application/json");

                var client = _clientFactory.CreateClient();

                var response = await client.PostAsync("https://localhost:44392/api/Students/", studentJson);

                var success = true;

                try
                {
                    response.EnsureSuccessStatusCode();
                } 
                catch (Exception)
                {
                    success = false;
                }

                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }
                //_context.Add(student);
                //await _context.SaveChangesAsync();
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var request = new HttpRequestMessage(HttpMethod.Get,
               $"https://localhost:44392/api/Students/{id}");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string jsonData = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Student>(jsonData);
                if (data == null)
                {
                    return NotFound();
                }
                return View(data);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentId,Name,ClassNumber,Grade")] Student student)
        {
            if (id != student.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //_context.Update(student);
                //await _context.SaveChangesAsync();
                var studentJson = new StringContent(
                    System.Text.Json.JsonSerializer.Serialize<Student>(student),
                    Encoding.UTF8,
                    "application/json");

                var client = _clientFactory.CreateClient();

                var response = await client.PutAsync($"https://localhost:44392/api/Students/{id}",
                    studentJson);
                var success = true;

                try
                {
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception)
                {
                    success = false;
                }
                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var request = new HttpRequestMessage(HttpMethod.Get,
               $"https://localhost:44392/api/Students/{id}");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string jsonData = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Student>(jsonData);
                if (data == null)
                {
                    return NotFound();
                }
                return View(data);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.DeleteAsync($"https://localhost:44392/api/Students/{id}");

            response.EnsureSuccessStatusCode();
            return RedirectToAction(nameof(Index));
        }
    }
}
