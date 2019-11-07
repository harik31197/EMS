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

        public static void UpdateUserInfo(string uname,int id)
        {
            using(EMSEntities db = new EMSEntities())
            {
                UserInfo user = new UserInfo();
                user.username = uname;
                user.Employee_emp_id = id;
                user.password = PasswordCrypt.Hash("456");

                db.UserInfoes.Add(user);
                db.SaveChanges();
            }
        }

        public static string InsertPassword(int id,SetPassword pword)
        {
            using(EMSEntities db = new EMSEntities())
            {
                var e = db.UserInfoes.Where(a => a.Employee_emp_id == id).FirstOrDefault();
                if (e != null)
                {
                    if (pword.password == pword.confirmpassword)
                    {
                        e.password = PasswordCrypt.Hash(pword.password);
                        if(id==1)
                        {
                            e.isAdmin = true;
                        }
                        else
                        {
                            e.isAdmin = false;
                        }
                        
                        db.SaveChanges();
                        db.UserInfoes.Add(e);
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

        public static string TryLogin(string username, string pword, out UserInfo user)
        {
            using (EMSEntities db = new EMSEntities())
            {
                user = db.UserInfoes.Where(a => a.username == username).FirstOrDefault();
                string e = PasswordCrypt.Hash(pword);
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
    }

   
}