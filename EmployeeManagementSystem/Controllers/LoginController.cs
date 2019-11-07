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

namespace EmployeeManagementSystem.Controllers
{
    public class LoginController : ApiController
    {
        // GET: api/Login
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

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
