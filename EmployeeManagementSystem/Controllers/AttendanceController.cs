using EmployeeManagementSystem.Helper_Classes;
using EmployeeManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace EmployeeManagementSystem.Controllers
{
    public class AttendanceController : ApiController
    {
        // GET: api/Attendance
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        
        [HttpGet]
        [Route("api/attendance/{id=id}")]
        [ResponseType(typeof(Attendance))]
        public IHttpActionResult GetAttendance(int id)
        {
            using (EMSEntities db = new EMSEntities())
            {
                var emp = db.Employees.Where(a => a.emp_id == id).FirstOrDefault();
                if (emp != null)
                {
                    try
                    {
                        var timeSheet = db.Attendances.Where(a => a.Employee_emp_id == id).Select(sheet => new
                        {
                            employee_id = sheet.Employee_emp_id,
                            dateofentry = sheet.date,
                            firstin = sheet.first_in.ToString(),
                            lastout = sheet.last_out.ToString(),
                            statusemp = sheet.status
                        });
                        return Ok(timeSheet.ToList());
                    }

                    catch(Exception e)
                    {
                        Logfile.WriteLogFile(e);
                        return BadRequest();
                    }
                    
                }

                else
                {
                    return BadRequest("EMployee Not Found");
                }
            }
        }
        [HttpGet]
        [Route("api/attendance")]
        public IHttpActionResult GetAllAttendances()
        {
            using (EMSEntities db = new EMSEntities())
            {
                var timeSheet = db.Attendances.Select(a => new
                {
                    empid = a.Employee_emp_id,
                    edate = a.date,
                    firstin = a.first_in.ToString(),
                    lastout = a.last_out.ToString(),
                    estatus = a.status
                });

                return Ok(timeSheet.ToList());
            }
        }

  
        

        // POST: api/Attendance
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Attendance/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Attendance/5
        public void Delete(int id)
        {
        }
    }
}
