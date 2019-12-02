using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace EmployeeManagementSystem.Helper_Classes
{
    public class VerificationMail
    {
        public static void SendVerificationEmail(string emailID, string code,string link, string emailFor)
        {        

            var fromEmail = new MailAddress("risenshine3197@gmail.com", "HR Admin");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "Admin@123";
            string subject = "";
            string body = "";
            if (emailFor == "VerifyAccount")
            {
                subject = "Your account is successfully created!";
                body = "<br/><br/>Your Employee account is successfully created. Please click on the below link to verify your account" +
            " " + " <br/><br/>" + link;
            }
           else if (emailFor == "ResetPassword")
            {
                subject = "Reset Password";               
                body = "Hi,<br/>br/>We got request for reset your account password. Please click on the below link to reset your password" +
                 "<br/><br/><a href=" + link + ">Reset Password Link</a>";
            }

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };
            using (var messager = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(messager);
        }

    }
}