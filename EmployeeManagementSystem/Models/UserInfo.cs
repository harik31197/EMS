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
    
    public partial class UserInfo
    {
        public string username { get; set; }
        public string password { get; set; }
        public Nullable<bool> isAdmin { get; set; }
        public string Resetcode { get; set; }
        public Nullable<int> ticket { get; set; }
        public int Employee_emp_id { get; set; }
        public Nullable<System.Guid> Activation_code { get; set; }
    
        public virtual Employee Employee { get; set; }
    }
}
