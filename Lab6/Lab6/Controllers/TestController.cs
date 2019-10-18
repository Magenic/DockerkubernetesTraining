using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab6.entities;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace Lab6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            var returnValues = new List<TestData>();
            using (MySqlConnection conn = new MySqlConnection($"server={ConfigurationValues.MySqlLocation};port=3306;database=foo;user={ConfigurationValues.MySqlUserId};password={ConfigurationValues.MySqlPwd}"))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from test_data", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        returnValues.Add(new TestData()
                        {
                            Name = reader["Name"].ToString(),
                            Value = reader["Value"].ToString()
                        });
                    }
                }
            }
            return JsonConvert.SerializeObject(returnValues);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
