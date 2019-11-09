using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EmployeeManagementSystem.Controllers
{
    public class LeaveController : ApiController
    {
        // GET: api/Leave
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Leave/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Leave
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Leave/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Leave/5
        public void Delete(int id)
        {
        }
    }
}
