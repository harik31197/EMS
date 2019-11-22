using EmployeeManagementSystem.Helper_Classes;
using EmployeeManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace EmployeeManagementSystem.Controllers
{
    public class LeaveController : ApiController
    {
        [HttpPost]
        [Route("api/leave/{id=id}")]
        [ResponseType(typeof(Leave))]

        public IHttpActionResult PostLeave(Leave leave)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            string message = DatabaseOps.LeaveChecker(leave);
            if(message == "Leave Successfully Applied")
            {
                return Ok(message);
            }

            else
            {
                return Ok(message);
            }
        }

        [HttpGet]
        [Route("api/leave/{id}/{leaveid}")]

        public IHttpActionResult BalanceLeave(int id,int leaveid)
        {
            var remainingLeaves = DatabaseOps.LeaveBalance(id, leaveid);
            return Ok("Leaves remaining " + remainingLeaves);
        }


        
    }
}
