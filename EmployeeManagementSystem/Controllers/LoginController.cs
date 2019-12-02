using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Helper_Classes;
using System.Security.Claims;
using System.Web.Security;
using System.Web;
using System.Web.Http.Cors;

namespace EmployeeManagementSystem.Controllers
{
    public class LoginController : ApiController
    {
        

        // GET: api/Login/5
       [AllowAnonymous]
        [HttpGet]
        [Route("api/login/test")]
        public IHttpActionResult Test()
        {
            return Ok("The time is" + DateTime.Now.ToString());
        }

        [Authorize]
        [HttpGet]
        [Route("api/login/authenticate")]
        public IHttpActionResult GetAuthenticate()
        {
            var identity = (ClaimsIdentity)User.Identity;

            return Ok("Hello User");
        }


        [Authorize(Roles = "admin")]
        [HttpGet]
        [Route("api/login/authorize")]
        public IHttpActionResult GetAdmin()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var roles = identity.Claims
                        .Where(c => c.Type == ClaimTypes.Role)
                        .Select(c => c.Value);
            return Ok("Hello" + identity.Name + "Role:" + string.Join(",",roles.ToList()));
        }

        [HttpGet]
        [Route("api/login/getid/{uname=uname}")]
        public IHttpActionResult GetID(string uname)
        {
            var empid = DatabaseOps.FindEmpId(uname);
            if (empid == 0)
                return Ok("Invalid Username");
            else
                return Ok(empid);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/forgotpassword")]
        public IHttpActionResult ForgotPassword(ForgotPassword fpword)
        {
           
            if(DatabaseOps.IsUsernameExist(fpword.username))
            {
                string Activation_code = Guid.NewGuid().ToString();
                var link = "http://localhost:4200//VerifyAccount/" + fpword.emp_id;
                VerificationMail.SendVerificationEmail(fpword.email, Activation_code, link, "ResetPassword");
                return Ok("Reset Mail sent");
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        [Route("api/forgotpassword/reset/{id=id}")]
        public IHttpActionResult ResetPassword(int id,SetPassword pword)
        {
            string message = DatabaseOps.InsertPassword(id,pword);
            if(message == "Success")
            {
               DatabaseOps.ResetPasswordinDB(id, pword);
                return Ok("Password reset Successfully");
            }
            else
            {
                return Ok("Error while resetting password");
            }
        }


        // POST: api/Login
        /* public IHttpActionResult LoginUser(user)
         {

         }*/

        // PUT: api/Login/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Login/5
        public void Delete(int id)
        {
        }
    }
}
