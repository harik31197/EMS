using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Helper_Classes
{
    public class DatabaseOps
    {
        public static bool IsSignUpSuccess(UserInfo user)
        {
            using (EMSEntities db = new EMSEntities())
            {
                var v = db.UserInfoes.Where(a => a.username == user.username).FirstOrDefault();
                if (v == null)
                {
                    db.UserInfoes.Add(user);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

        public static bool IsEmailExist(string emailID)
        {
            using (EMSEntities db = new EMSEntities())
            {
                var v = db.Employees.Where(e => e.emp_email == emailID).FirstOrDefault();
                return v != null;
            }
        }

        public static bool IsUsernameExist(string uname)
        {
            using(EMSEntities db = new EMSEntities())
            {
                var z = db.Employees.Where(a => a.username == uname).FirstOrDefault();
                return z != null;
            }
        }
      

        public static void UpdateUserInfo(int id,SetPassword pword)
        {
            using(EMSEntities db = new EMSEntities())
            {
                var e = db.Employees.Where(a => a.emp_id == id).FirstOrDefault();
                if(e!=null)
                {
                    UserInfo user = new UserInfo();
                    if (e.emp_id == 1)
                    {
                        user.isAdmin = true;
                    }
                    else
                    {
                        user.isAdmin = false;
                    }
                    //e.isEmailVerified = true;
                    user.username = e.username;
                    user.Employee_emp_id = id;
                    user.password = PasswordCrypt.Hash(pword.password);
                    db.UserInfoes.Add(user);
                    db.SaveChanges();
                }                
            }
        }
        

        public static string InsertPassword(int id,SetPassword pword)
        {
            using(EMSEntities db = new EMSEntities())
            {               
                var e = db.Employees.Where(a => a.emp_id == id).FirstOrDefault();
                if (e != null)
                {
                    if (pword.password == pword.confirmpassword)
                    {                   
                      return "Success";
                    }
                    else
                    {
                        return "Unmatching Passwords";
                    }
                }
                else
                {
                    return "Not Success";
                }
            }
        }
        public static void ResetPasswordinDB(int id,SetPassword pword)
        {
            using(EMSEntities db = new EMSEntities())
            {
                var e = db.UserInfoes.Where(a => a.Employee_emp_id == id).FirstOrDefault();
                if(e!=null)
                {
                    e.password = PasswordCrypt.Hash(pword.password);
                    db.SaveChanges();
                    
                }              

            }
        }
  

        public static string TryLogin(string username, string pword, out UserInfo user)
        {
            using (EMSEntities db = new EMSEntities())
            {
                user = db.UserInfoes.Where(a => a.username == username).FirstOrDefault();                
                if (user != null)
                {
                    if (String.Compare(user.password, PasswordCrypt.Hash(pword)) == 0)
                    {
                        return "Success";
                    }
                    else
                    {
                        user = null;
                        return "Password is Wrong";
                    }
                }
                else
                {
                    user = null;
                    return "Invalid Credentials";
                }

            }
        }

        public static double LeaveBalance(int id, int leaveid)
        {
            double rem = 0.0;
            using (EMSEntities db = new EMSEntities())
            {
                var user = db.Employees.Where(a => a.emp_id == id).FirstOrDefault();
                var userList = from e in db.Leave_history
                               join employee in db.Employees on e.Employee_emp_id equals employee.emp_id
                               select new
                               {
                                   uname = employee.username,
                                   remaining = e.Leaves_remaining,
                                   leavetype = e.Leave_type_type_id,
                                   empid = employee.emp_id
                               };
                foreach (var emp in userList)
                {
                    if (user.username == emp.uname)
                    {
                        if (emp.leavetype == leaveid)
                        {
                            rem = emp.remaining;
                            break;
                        }
                    }
                }

                return rem;
            }
        }

        public static string LeaveChecker(Leave leave)
        {
            bool isApply = false;
            EMSEntities db = new EMSEntities();
            var user = db.Employees.Where(a => a.emp_id == leave.Employee_emp_id).FirstOrDefault();
            var userList = from e in db.Leave_history
                           join employee in db.Employees on e.Employee_emp_id equals employee.emp_id
                           select new
                           {
                               uname = employee.username,
                               remaining = e.Leaves_remaining,
                               leavetype = e.Leave_type_type_id,
                               empid = employee.emp_id
                           };
            foreach(var emp in userList)
            {
                if(user.username == emp.uname)
                {
                    if(leave.Leave_type_type_id == emp.leavetype)
                    {
                        double appliedDays = (leave.to_date.Subtract(leave.from_date)).Days;
                        if (leave.from_session == leave.to_session)
                            appliedDays = appliedDays + 0.5;
                        double remainingDays = emp.remaining - appliedDays;
                        if (appliedDays < emp.remaining)
                        {
                            db.Leaves.Add(leave);
                            var leave_history = db.Leave_history.Where(q => q.Employee_emp_id == emp.empid && q.Leave_type_type_id == emp.leavetype).FirstOrDefault();
                            leave_history.Leaves_remaining = remainingDays;
                            leave.leave_status = "Applied";
                            isApply = true;
                            break;
                        }                          
                    }
                }
            }
            if(isApply)
            {
                try
                {
                    db.SaveChanges();
                }
                catch(Exception e)
                {
                    Logfile.WriteLogFile(e);
                }

                return "Leave Successfully Applied";
            }
            else
            {
                return "Leave Could not be Applied";
            }
        }

    
    }

   
}

/* public static string ApplyLeave(LeaveApplication leave)
        {
            PsiogEntities PE = new PsiogEntities();
            var leaveBalance = PE.EmployeeLeaveAvaliabilities.Where(emp => emp.EmployeeId == leave.EmployeeId && emp.LeaveTypeId == leave.LeaveId).FirstOrDefault();


            decimal availedDays = (leave.ToDate.Subtract(leave.FromDate)).Days;
            if (leave.FromSession == leave.ToSession)
                availedDays = availedDays + 0.5M;
            decimal balance = leaveBalance.AllocatedDays - leaveBalance.AvailedDays;
            if (availedDays <= balance)
            {
                leave.Status = "Applied";
                PE.LeaveApplications.Add(leave);
                leaveBalance.AvailedDays = availedDays;
                PE.SaveChanges();
                return "Leave Application Submitted! Waiting For Approval";
            }

            else
            {
                return "You do not have enough leave balance";
            }
        }*/
