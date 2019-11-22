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
        [HttpGet]
        [Route("api/check")]
        public IHttpActionResult Hello()
        {
            return Ok("Hello from postman!The time is " + DateTime.Now);
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
            var link = HttpContext.Current.Request.Url.AbsoluteUri + "/VerifyAccount/" + emp.emp_id;
            VerificationMail.SendVerificationEmail(emp.emp_email, Activation_code, link, "VerifyAccount");
            db.Employees.Add(emp);
            db.SaveChanges();         //Employee table updated
           // DatabaseOps.UpdateUserInfo(emp.username,emp.emp_id);    //Updating username to userinfo table
            StoredProcs.AddLeaveEntry(emp);  //Adding Leave data for new employee
            return Ok("Employee added Successfully");

        }

        [HttpPost]
        [Route("api/signup/addemployee/VerifyAccount/{id=id}")]
        public IHttpActionResult VerifyEmployee(int id, SetPassword password)
        {
            string message = DatabaseOps.InsertPassword(id,password);

            if (message == "Success")
            {
                DatabaseOps.UpdateUserInfo(id, password);              
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
