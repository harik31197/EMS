using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Helper_Classes
{
    public class StoredProcs
    {
        public static void AddUserDetails(int id,SetPassword pword,bool isAdmin)
        {
            using(EMSEntities db = new EMSEntities())
            {
                var emp = db.Employees.Where(a => a.emp_id == id).FirstOrDefault();
                var user = db.AddUserInfo(emp.emp_id, emp.username,pword.password,isAdmin);
            }
        }

        public static void AddLeaveEntry(Employee emp)
        {
            using (EMSEntities db = new EMSEntities())
            {
                var leave = db.AddLeaveEntry(emp.emp_joiningdate,emp.emp_id);
            }
        }
    }
}