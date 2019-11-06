using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using EmployeeManagementSystem.Helper_Classes;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Controllers
{
    public class UserController : ApiController
    {

        private EMSEntities db = new EMSEntities();

        public IQueryable<Employee> GetEmployees()
        {
            return db.Employees;
        }
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
        [Route("api/signup/addemployee")]
        [ResponseType(typeof(Employee))]
        public IHttpActionResult PostEmployee(Employee emp)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (DatabaseOps.IsUsernameExist(emp.username))
                {
                    return Conflict();     //Duplicate username found in DB
                }
                if (DatabaseOps.IsEmailExist(emp.emp_email))
                {
                    return Conflict();    //Duplicate Email found
                }
            }
               
           
            catch(Exception e)
            {
                throw e;                
            }
            
            emp.isEmailVerified = false;
            string Activation_code = Guid.NewGuid().ToString();
            var link = HttpContext.Current.Request.Url.AbsoluteUri + "/VerifyAccount/" + emp.username;
            VerificationMail.SendVerificationEmail(emp.emp_email, Activation_code,link, "VerifyAccount");
            db.Employees.Add(emp);
            db.SaveChanges();         //Employee table updated
            DatabaseOps.UpdateUserInfo(emp.username,emp.emp_id);    //Updating username to userinfo table
            return Ok("Employee added");

        }

        [HttpPost]
        [Route("api/signup/addemployee/VerifyAccount/{id=id}")]
        public IHttpActionResult VerifyEmployee(string id, SetPassword password)
        {
            string message = DatabaseOps.InsertPassword(id,password);
            if (message == "Success")
            {
                return Ok("Verified Successfully");
            }
            else
            {
                return Ok("Verification Not Successful");
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
