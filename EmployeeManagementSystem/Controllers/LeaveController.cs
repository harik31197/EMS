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
        private EMSEntities db = new EMSEntities();
        [HttpPost]
        [Route("api/leave/{id}")]
        [ResponseType(typeof(Leave))]

        public IHttpActionResult PostLeave(int id,Leave leave)
        {
            leave.Employee_emp_id = id;
           // leave.Submited_date = DateTime.Now;
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
            var leaveType = db.Leave_type.Where(a => a.type_id == leaveid).FirstOrDefault();
            return Ok(remainingLeaves);
        }


        
    }
}
