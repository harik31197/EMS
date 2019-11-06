using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeManagementSystem.Helper_Classes;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Controllers
{
    public class UserController : ApiController
    {
        // GET: api/User
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/User/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/User
        [HttpPost]
        public IHttpActionResult PostEmployee(Employee emp, UserInfo user)
        {
            if(ModelState.IsValid)
            {
                var isExist = DatabaseOps.IsEmailExist(emp.emp_email);
                if(isExist)
                {
                    {

                    } }
            }
        }

        // PUT: api/User/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/User/5
        public void Delete(int id)
        {
        }
    }
}
