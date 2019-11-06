using System;
using System.Text;

namespace EmployeeManagementSystem.Helper_Classes
{
    public class PasswordCrypt
    {
        public static string Hash(string value)
        {
            return Convert.ToBase64String(
                System.Security.Cryptography.SHA256.Create()
                .ComputeHash(Encoding.UTF8.GetBytes(value))
                );
        }
    }
}