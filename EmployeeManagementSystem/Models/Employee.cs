//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EmployeeManagementSystem.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Employee
    {
        public Nullable<int> emp_age { get; set; }
        public int emp_id { get; set; }
        public System.DateTime dob { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string emp_address { get; set; }
        public string emp_email { get; set; }
        public string emp_bloodgroup { get; set; }
        public Nullable<long> emp_mobile { get; set; }
        public System.DateTime emp_joiningdate { get; set; }
        public Nullable<System.DateTime> emp_relievingdate { get; set; }
        public string emp_dept { get; set; }
        public string emp_desig { get; set; }
        public string username { get; set; }
        public Nullable<bool> isEmailVerified { get; set; }
    
        public virtual UserInfo UserInfo { get; set; }
    }
}
