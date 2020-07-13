using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Agate_Model;
using Microsoft.AspNetCore.Mvc;
using Simple.OData.Client;

namespace Agate_OData_Client
{
    class Program
    {

        static async Task<IEnumerable<Student>> fetchData(int id)
        {
            var client = new ODataClient("https://localhost:44391/odata/");
            var student = await client
                                    .For<Student>()
                                    .Key(id)
                                    .FindEntriesAsync();

            return student;
        }
        static void Main(string[] args)
        {
            var student = fetchData(1).Result.ToString();

            Console.WriteLine(student);
        }
    }
}
